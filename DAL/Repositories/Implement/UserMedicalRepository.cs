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
                .Include(um => um.Bloods)
                .Where(um => um.Type == Common.Enum.MedicalType.AVAILABLE)
                .ToListAsync();
        }

        public async Task<Guid?> GetUserMedicalIdAsync(Guid userId)
        {
            var userMedical = await _context.UserMedicals
                .Where(um => um.UserId == userId)
                .Select(um => (Guid?)um.UserMedicalId) // nullable Guid
                .FirstOrDefaultAsync();

            return userMedical; // nếu không có thì trả về null
        }
        public async Task<UserMedical> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserMedicals
                .FirstOrDefaultAsync(um => um.UserId == userId);
        }

    }
}
