using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<Location> GetOrCreateAsync(string address);
    }
}