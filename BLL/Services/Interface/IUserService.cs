using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        Task<ResponseDTO> DeleteUserAsync(Guid userId);

        Task<ResponseDTO> GetAllUserAsync();
        Task<ResponseDTO> GetUserByIdAsync(Guid userId);

    }
}
