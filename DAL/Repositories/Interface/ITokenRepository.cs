using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ITokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByUserID(Guid userId);
        Task<RefreshToken?> GetRefreshTokenByKey(string refreshTokenKey);

        Task CreateTokenAsync(RefreshToken token);
        Task RevokeTokenAsync(RefreshToken token);
        Task<RefreshToken> GetActiveTokenAsync(Guid userId);
    }
}
