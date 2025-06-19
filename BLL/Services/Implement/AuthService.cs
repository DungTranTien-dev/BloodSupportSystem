using Azure.Identity;
using BLL.Services.Interface;
using Common.Constants;
using Common.DTO;
using DAL.Models;
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
        private readonly IUnitOfWork _unitOfWork;
        public AuthService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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


            //kiểm tra refreshToken
            var exitsRefreshToken = await _unitOfWork.TokenRepo.GetRefreshTokenByUserID(user.UserId);
            if (exitsRefreshToken != null)
            {
                exitsRefreshToken.IsRevoked = true;
                await _unitOfWork.TokenRepo.UpdateAsync(exitsRefreshToken);
            }

            //khởi tạo claim
            var claims = new List<Claim>
                {
                    new Claim(JwtConstant.KeyClaim.Email, user.Email),
                    new Claim(JwtConstant.KeyClaim.UserId, user.UserId.ToString()),
                    new Claim(JwtConstant.KeyClaim.UserName, user.UserName),
                    //new Claim(JwtConstant.KeyClaim.Role, user.Role.ToString())
                };

            //tạo refesh token
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

            return new ResponseDTO("Đăng nhập thành công" ,200, true, new
            {
                AccessToken = accessTokenKey,
                RefeshToken = refreshTokenKey,
            });

        }

        public async Task<ResponseDTO> Register(RegisterDTO registerDTO)
        {
            // Kiểm tra username không được null hoặc rỗng
            if (string.IsNullOrWhiteSpace(registerDTO.UserName ))
            {
                return new ResponseDTO("Username is required.",400, false);
            }

            // Kiểm tra email không được null hoặc rỗng
            if (string.IsNullOrWhiteSpace(registerDTO.Email))
            {
                return new ResponseDTO("Email is required.", 400, false);
            }

            // Kiểm tra định dạng email hợp lệ
            if (!IsValidEmail(registerDTO.Email))
            {
                return new ResponseDTO("Invalid email format.", 400, false);
            }

            // Kiểm tra trùng email
            var existingUser = await _unitOfWork.UserRepo.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            { 

            
                return new ResponseDTO("Email is already registered.", 400, false);
            }

            // Kiểm tra mật khẩu không được null
            if (string.IsNullOrWhiteSpace(registerDTO.Password))
            {
                return new ResponseDTO("Password is required.", 400, false);
            }

            // Kiểm tra xác nhận mật khẩu
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return new ResponseDTO("Passwords do not match.", 400, false);
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            // Tạo người dùng mới
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
