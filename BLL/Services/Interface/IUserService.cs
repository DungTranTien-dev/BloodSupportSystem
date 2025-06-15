using BLL.DTOs;
using DAL.Models;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserCreateDto dto);
    }
}