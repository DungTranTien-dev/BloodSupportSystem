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

        public async Task<IEnumerable<UserMedical>> GetAllAsync()
            => await _context.UserMedicals.Include(u => u.Blood).ToListAsync();

        public async Task<UserMedical?> GetByIdAsync(Guid id)
            => await _context.UserMedicals.FirstOrDefaultAsync(u => u.UserMedicalId == id);

        public async Task AddAsync(UserMedical entity)
            => await _context.UserMedicals.AddAsync(entity);

        public void Update(UserMedical entity)
            => _context.UserMedicals.Update(entity);

        public void Delete(UserMedical entity)
            => _context.UserMedicals.Remove(entity);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }

}
