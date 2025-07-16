using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class UserMedicalRepository : GenericRepository<UserMedical>, IUserMedicalRepository
    {
        public readonly ApplicationDbContext _context;

        public UserMedicalRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserMedical>> GetAllAvailableWithBloodAsync()
        {
            return await _context.UserMedicals
                .Include(um => um.Blood)
                .Where(um => um.Blood.IsAvailable == true)
                .ToListAsync();
        }

    public Task<UserMedical?> GetByUserIdAsync(Guid userId)
    {
        return _context.UserMedicals
            .Include(um => um.User)
            .Include(um => um.Blood)
            .Include(um => um.UserMedicalChronicDiseases)
                .ThenInclude(umcd => umcd.ChronicDisease)
            .FirstOrDefaultAsync(um => um.UserId == userId);
    }

    public async Task<bool> HasUserMedicalAsync(Guid userId)
        {
            return await _context.UserMedicals
                .AnyAsync(um => um.UserId == userId);
        }
    }
}
