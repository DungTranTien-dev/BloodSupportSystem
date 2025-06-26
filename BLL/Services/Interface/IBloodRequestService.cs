using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBloodRequestService
    {
        Task<List<BloodRequestDTO>> GetByUserAsync(Guid userId);
        Task<List<BloodRequestDTO>> GetAllAsync();
        Task<bool> CreateAsync(CreateBloodRequestDTO dto);
        Task<bool> UpdateAsync(Guid id, CreateBloodRequestDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }

}
