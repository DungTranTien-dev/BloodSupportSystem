using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class BloodRepository : GenericRepository<Blood> , IBloodRepository
    {
        private readonly ApplicationDbContext _context;

        public BloodRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Blood> CreateBloodAsync(Blood blood)
        {
            _context.Blood.Add(blood);
            await _context.SaveChangesAsync();
            return blood;
        }
        public async Task<Blood> GetBloodByIdAsync(Guid id)
        {
            return await _context.Blood.FindAsync(id);
        }

        public async Task<IEnumerable<Blood>> GetAllAsync()
        {
            return await _context.Blood.ToListAsync();
        }

        public async Task<Blood> UpdateAsync(Blood blood)
        {
            _context.Blood.Update(blood);
            await _context.SaveChangesAsync();
            return blood;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var blood = await _context.Blood.FindAsync(id);
            if (blood == null) return false;

            _context.Blood.Remove(blood);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}