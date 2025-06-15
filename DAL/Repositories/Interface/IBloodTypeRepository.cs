using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IBloodTypeRepository : IGenericRepository<BloodType>
    {
        Task<BloodType> GetByNameAsync(string name);
    }
}