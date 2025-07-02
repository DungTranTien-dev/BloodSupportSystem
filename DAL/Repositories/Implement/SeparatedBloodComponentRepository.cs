using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class SeparatedBloodComponentRepository : GenericRepository<SeparatedBloodComponent>, ISeparatedBloodComponentRepository
    {
        private readonly ApplicationDbContext _context;
        public SeparatedBloodComponentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<SeparatedBloodComponent>> GetAllWithBloodAsync()
        {
            return await _context.SeparatedBloodComponents
                .Include(c => c.Blood)
                .ToListAsync();
        }

        public async Task<IEnumerable<SeparatedBloodComponent>> GetAllAsync(Expression<Func<SeparatedBloodComponent, bool>> predicate)
        {
            return await _context.Set<SeparatedBloodComponent>()
                .Include(c => c.Blood) // để lấy Blood.BloodName so sánh
                .Where(predicate)
                .ToListAsync();
        }

    }
}
