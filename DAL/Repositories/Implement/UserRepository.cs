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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public readonly ApplicationDbContext _context;

        public UserRepository (ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
