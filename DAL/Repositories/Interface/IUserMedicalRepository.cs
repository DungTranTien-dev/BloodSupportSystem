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
        Task<bool> HasUserMedicalAsync(Guid userId);
        Task<UserMedical?> GetByUserIdAsync(Guid userId);
        new Task UpdateAsync(UserMedical userMedical);
    }
}
