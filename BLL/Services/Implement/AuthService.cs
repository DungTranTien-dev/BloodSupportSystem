using BLL.Services.Interface;
using Common.Constants;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.Repositories.Interface;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Common.DTO.AuthDTO;

namespace BLL.Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenRepository _tokenRepo;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IBloodTypeRepository _bloodTypeRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IUserRepository userRepo,
            ITokenRepository tokenRepo,
            IUnitOfWork unitOfWork,
            IGoogleAuthService googleAuthService,
            IBloodTypeRepository bloodTypeRepo,
            ILocationRepository locationRepo)
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
            _unitOfWork = unitOfWork;
            _googleAuthService = googleAuthService;
            _bloodTypeRepo = bloodTypeRepo;
            _locationRepo = locationRepo;
        }
        public async Task<ResponseDTO> Login(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.UserRepo.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return new ResponseDTO("khong co user", 400, false);
            }

            // kiem tra password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return new ResponseDTO("pass sai", 400, false);
            }

            //kiểm tra refreshToken
            var exitsRefreshToken = await _unitOfWork.TokenRepo.GetRefreshTokenByUserID(user.Id);
            if (exitsRefreshToken != null)
            {
                exitsRefreshToken.IsRevoked = true;
                await _unitOfWork.TokenRepo.UpdateAsync(exitsRefreshToken);
            }

            //khởi tạo claim
            var claims = new List<Claim>
                {
                    new Claim(JwtConstant.KeyClaim.Email, user.Email),
                    new Claim(JwtConstant.KeyClaim.UserId, user.Id.ToString()),
                    new Claim(JwtConstant.KeyClaim.UserName, user.FullName),
                    //new Claim(JwtConstant.KeyClaim.Role, user.Role.ToString())
                };

            //tạo refesh token
            var refreshTokenKey = JwtProvider.GenerateRefreshToken(claims);
            var accessTokenKey = JwtProvider.GenerateAccessToken(claims);

            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.Id,
                RefreshTokenKey = refreshTokenKey,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };
            _unitOfWork.TokenRepo.Add(refreshToken);
            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving refresh token: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Đăng nhập thành công", 200, true, new
            {
                AccessToken = accessTokenKey,
                RefeshToken = refreshTokenKey,
            });

        }
        public async Task<ResponseDTO> GoogleSignInAsync(GoogleSignInDTO dto)
        {
            try
            {
                // Use GoogleAuthService to validate token
                var payload = await _googleAuthService.ValidateTokenAsync(dto.GoogleToken);
                if (payload == null)
                    return new ResponseDTO("Invalid Google token", 401, false);

                // Check if user exists
                var user = await _userRepo.FindByEmailAsync(payload.Email);
                if (user != null)
                    return await GenerateAuthResponse(user);

                // New user needs to complete registration
                return new ResponseDTO("Complete registration", 202, true, new
                {
                    Email = payload.Email,
                    GoogleToken = dto.GoogleToken,
                    SuggestedValues = new
                    {
                        FullName = payload.Name ?? "Your Name",
                        PhoneNumber = "123-456-7890",
                        BloodTypeName = "A+",
                        Address = "Your Address"
                    }
                });
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Google sign-in failed: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> CompleteGoogleSignUpAsync(GoogleSignUpCompleteDTO dto)
        {
            try
            {
                // Validate Google token using GoogleAuthService
                var payload = await _googleAuthService.ValidateTokenAsync(dto.GoogleToken);
                if (payload == null)
                    return new ResponseDTO("Invalid Google token", 401, false);

                // Verify email matches
                if (payload.Email != dto.Email)
                    return new ResponseDTO("Email does not match Google account", 400, false);

                // Get blood type
                var bloodType = await _bloodTypeRepo.GetByNameAsync(dto.BloodTypeName);
                if (bloodType == null)
                    return new ResponseDTO("Invalid blood type", 400, false);

                // Create location
                var location = await _locationRepo.GetOrCreateAsync(dto.Address);

                // Create user
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = dto.Email,
                    FullName = dto.FullName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    PhoneNumber = dto.PhoneNumber,
                    BloodTypeId = bloodType.Id,
                    LocationId = location.Id,
                    Role = UserRole.Member,
                    CreatedAt = DateTime.UtcNow,
                    LastDonationDate = DateTime.MinValue
                };

                await _userRepo.CreateAsync(user);
                return await GenerateAuthResponse(user);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Registration failed: {ex.Message}", 500, false);
            }
        }

        private async Task<ResponseDTO> GenerateAuthResponse(User user)
        {
            // Revoke existing tokens
            var existingToken = await _tokenRepo.GetActiveTokenAsync(user.Id);
            if (existingToken != null)
                await _tokenRepo.RevokeTokenAsync(existingToken);

            // Generate claims
            var claims = new List<Claim>
            {
                new Claim(JwtConstant.KeyClaim.UserId, user.Id.ToString()),
                new Claim(JwtConstant.KeyClaim.Email, user.Email),
                new Claim(JwtConstant.KeyClaim.UserName, user.FullName),
                new Claim(JwtConstant.KeyClaim.Role, user.Role.ToString())
            };

            // Generate tokens using your existing JwtProvider
            var accessToken = JwtProvider.GenerateAccessToken(claims);
            var refreshToken = JwtProvider.GenerateRefreshToken(claims);

            // Save refresh token
            await _tokenRepo.CreateTokenAsync(new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.Id,
                RefreshTokenKey = refreshToken,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            });

            return new ResponseDTO("Authentication successful", 200, true, new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserInfo = new
                {
                    user.Id,
                    user.Email,
                    user.FullName,
                    user.Role
                }
            });
        }
}
}
