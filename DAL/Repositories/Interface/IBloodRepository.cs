using DAL.Models;
using DAL.Repositories.Interface;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IBloodRepository : IGenericRepository<Blood>
    {
        Task<Blood> CreateBloodAsync(Blood blood);
        Task<Blood> GetBloodByIdAsync(Guid id);
        Task<IEnumerable<Blood>> GetAllAsync();
        Task<Blood> UpdateAsync(Blood blood);
        Task<bool> DeleteAsync(Guid id);
    }
}