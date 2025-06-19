using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IUserMedicalService
    {
        Task<IEnumerable<UserMedicalDTO>> GetAllAsync();
        Task<UserMedicalDTO?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(UserMedicalDTO dto);
        Task<bool> UpdateAsync(UserMedicalDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }

}
