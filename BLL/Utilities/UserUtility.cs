using BLL.Services.Implement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
    public class UserUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserIDFromToken()
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
                if (string.IsNullOrEmpty(token))
                    return Guid.Empty;

                var claims = JwtProvider.DecodeToken(token);
                var userIdClaim = claims.FirstOrDefault(c => c.Type == "UserId" || c.Type == "sub");

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return userId;
                }

                return Guid.Empty;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
