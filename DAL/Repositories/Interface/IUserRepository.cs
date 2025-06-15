using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> FindByEmailAsync(string email);
    }
}