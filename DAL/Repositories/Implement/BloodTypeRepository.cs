using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class BloodTypeRepository : GenericRepository<BloodType>, IBloodTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public BloodTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BloodType> GetByNameAsync(string name)
        {
            return await _context.BloodTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(bt => bt.Name == name);
        }
    }
}