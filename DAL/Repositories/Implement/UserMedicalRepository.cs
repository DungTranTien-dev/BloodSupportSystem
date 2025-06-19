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
    public class UserMedicalRepository : IUserMedicalRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMedicalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserMedical> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserMedicals
                .FirstOrDefaultAsync(um => um.UserId == userId);
        }

        public async Task UpdateAsync(UserMedical userMedical)
        {
            _context.UserMedicals.Update(userMedical);
            await _context.SaveChangesAsync();
        }
    }
}
