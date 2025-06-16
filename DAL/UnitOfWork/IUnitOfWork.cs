using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepo { get; }
        ITokenRepository TokenRepo { get; }
        IBloodRegistrationRepository BloodRegistrationRepo { get; }
        IEventRepository EventRepo { get; }
        Task<int> SaveAsync();
        Task<bool> SaveChangeAsync();
    }
}
