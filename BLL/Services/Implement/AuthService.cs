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
    }
}
