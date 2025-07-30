using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IUserMedicalRepository  : IGenericRepository<UserMedical>
    {
        Task<List<UserMedical>> GetAllAvailableWithBloodAsync();
        Task<UserMedical> GetByUserIdAsync(Guid userId);
        Task<Guid?> GetUserMedicalIdAsync(Guid userId);
    }
}
