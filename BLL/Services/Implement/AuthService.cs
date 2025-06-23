using Azure.Identity;
using BLL.Services.Interface;
using Common.Constants;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.UnitOfWork;
using Google.Apis.Auth;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGoogleAuthService _googleAuthService;

        public AuthService(IUnitOfWork unitOfWork, IGoogleAuthService googleAuthService)
        {
            _unitOfWork = unitOfWork;
            _googleAuthService = googleAuthService;
        }

        public async Task<ResponseDTO> Login(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.UserRepo.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return new ResponseDTO("khong co user", 400, false);
            }

            try
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                if (!isPasswordValid)
                {
                    return new ResponseDTO("Sai mật khẩu", 400, false);
                }
            }
            catch (BCrypt.Net.SaltParseException ex)
            {
                return new ResponseDTO("Mật khẩu lưu không hợp lệ (lỗi hệ thống)", 500, false);
            }

            var exitsRefreshToken = await _unitOfWork.TokenRepo.GetRefreshTokenByUserID(user.UserId);
            if (exitsRefreshToken != null)
            {
                exitsRefreshToken.IsRevoked = true;
                await _unitOfWork.TokenRepo.UpdateAsync(exitsRefreshToken);
            }

            var claims = new List<Claim>
            {
                new Claim(JwtConstant.KeyClaim.Email, user.Email),
                new Claim(JwtConstant.KeyClaim.UserId, user.UserId.ToString()),
                new Claim(JwtConstant.KeyClaim.UserName, user.UserName),
            };

            var refreshTokenKey = JwtProvider.GenerateRefreshToken(claims);
            var accessTokenKey = JwtProvider.GenerateAccessToken(claims);

            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserId,
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
                var payload = await _googleAuthService.ValidateTokenAsync(dto.GoogleToken);
                if (payload == null)
                    return new ResponseDTO("Invalid Google token", 401, false);

                var user = await _unitOfWork.UserRepo.FindByEmailAsync(payload.Email);
                if (user == null && !string.IsNullOrEmpty(payload.Subject))
                {
                    user = await _unitOfWork.UserRepo.FindByGoogleIdAsync(payload.Subject);
                }

                if (user != null)
                {
                    return await GenerateAuthResponse(user);
                }

                return new ResponseDTO("Complete registration", 202, true, new
                {
                    Email = payload.Email,
                    GoogleToken = dto.GoogleToken,
                    SuggestedValues = new
                    {
                        FullName = payload.Name ?? "",
                        PhoneNumber = "",
                        BloodTypeName = "",
                        Address = "",
                        DateOfBirth = DateTime.UtcNow,
                        Gender = 0,
                        CitizenId = "",
                        Province = "",
                        HasDonatedBefore = false,
                        DiseaseDescription = ""
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
                var payload = await _googleAuthService.ValidateTokenAsync(dto.GoogleToken);
                if (payload == null)
                    return new ResponseDTO("Invalid Google token", 401, false);

                if (payload.Email != dto.Email)
                    return new ResponseDTO("Email does not match Google account", 400, false);

                var existingUser = await _unitOfWork.UserRepo.FindByEmailAsync(dto.Email);
                if (existingUser != null)
                    return new ResponseDTO("Email is already registered.", 400, false);

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                var blood = await _unitOfWork.BloodRepo.GetByNameAsync(dto.BloodTypeName);
                if (blood == null)
                    return new ResponseDTO("Invalid blood type", 400, false);

                var newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = dto.FullName,
                    Email = dto.Email,
                    Password = passwordHash,
                    GoogleId = payload.Subject
                };
                await _unitOfWork.UserRepo.AddAsync(newUser);

                var userMedical = new UserMedical
                {
                    UserMedicalId = Guid.NewGuid(),
                    FullName = dto.FullName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = (Gender)dto.Gender,
                    CitizenId = dto.CitizenId,
                    BloodId = blood.BloodId,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Province = dto.Province,
                    CurrentAddress = dto.CurrentAddress,
                    HasDonatedBefore = dto.HasDonatedBefore,
                    DiseaseDescription = dto.DiseaseDescription,
                    CreateDate = DateTime.UtcNow,
                    UserId = newUser.UserId
                };
                await _unitOfWork.UserMedicalRepo.AddAsync(userMedical);

                await _unitOfWork.SaveChangeAsync();

                return await GenerateAuthResponse(newUser);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Registration failed: {ex.Message}", 500, false);
            }
        }

        private async Task<ResponseDTO> GenerateAuthResponse(User user)
        {
            var existingToken = await _unitOfWork.TokenRepo.GetRefreshTokenByUserID(user.UserId);
            if (existingToken != null)
            {
                existingToken.IsRevoked = true;
                await _unitOfWork.TokenRepo.UpdateAsync(existingToken);
            }

            var claims = new List<Claim>
            {
                new Claim(JwtConstant.KeyClaim.UserId, user.UserId.ToString()),
                new Claim(JwtConstant.KeyClaim.Email, user.Email),
                new Claim(JwtConstant.KeyClaim.UserName, user.UserName)
            };

            var accessToken = JwtProvider.GenerateAccessToken(claims);
            var refreshToken = JwtProvider.GenerateRefreshToken(claims);

            var refreshTokenEntity = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserId,
                RefreshTokenKey = refreshToken,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };
            _unitOfWork.TokenRepo.Add(refreshTokenEntity);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Authentication successful", 200, true, new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserInfo = new
                {
                    user.UserId,
                    user.Email,
                    user.UserName
                }
            });
        }

        public async Task<ResponseDTO> Register(RegisterDTO registerDTO)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.UserName))
            {
                return new ResponseDTO("Username is required.", 400, false);
            }

            if (string.IsNullOrWhiteSpace(registerDTO.Email))
            {
                return new ResponseDTO("Email is required.", 400, false);
            }

            if (!IsValidEmail(registerDTO.Email))
            {
                return new ResponseDTO("Invalid email format.", 400, false);
            }

            var existingUser = await _unitOfWork.UserRepo.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                return new ResponseDTO("Email is already registered.", 400, false);
            }

            if (string.IsNullOrWhiteSpace(registerDTO.Password))
            {
                return new ResponseDTO("Password is required.", 400, false);
            }

            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return new ResponseDTO("Passwords do not match.", 400, false);
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Password = passwordHash
            };

            await _unitOfWork.UserRepo.AddAsync(newUser);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Registration successful.", 200, true);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
