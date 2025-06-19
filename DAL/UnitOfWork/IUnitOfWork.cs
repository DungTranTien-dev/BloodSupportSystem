using DAL.Repositories.Interface;
using DAL.Repositories.Interfaces;
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
        IChronicDiseaseRepository ChronicDiseaseRepo { get; }
        IUserMedicalRepository UserMedicalRepo { get; }
        IBloodRepository BloodRepo { get; }

        Task<int> SaveAsync();
        Task<bool> SaveChangeAsync();
    }
}
