using Common.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IBloodRegistrationRepository : IGenericRepository<BloodRegistration>
    {
        Task<List<RegistrationByUserIdDTO>> GetByUserIdAsync(Guid userId);
        Task<List<BloodRegistration>> GetAllRegistration();
    }
}
