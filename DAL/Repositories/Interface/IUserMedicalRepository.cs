using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IUserMedicalRepository
    {
        Task<IEnumerable<UserMedical>> GetAllAsync();
        Task<UserMedical?> GetByIdAsync(Guid id);
        Task AddAsync(UserMedical entity);
        void Update(UserMedical entity);
        void Delete(UserMedical entity);
        Task SaveAsync();
    }

}
