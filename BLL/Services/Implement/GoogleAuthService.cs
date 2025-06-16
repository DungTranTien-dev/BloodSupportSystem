using BLL.Services.Interface;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IConfiguration _config;

        public GoogleAuthService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<GoogleJsonWebSignature.Payload> ValidateTokenAsync(string token)
        {
            try
            {
                return await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _config["Google:ClientId"] }
                });
            }
            catch
            {
                return null;
            }
        }
    }
}