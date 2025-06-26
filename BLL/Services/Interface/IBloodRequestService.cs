using Common.DTO;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBloodRequestService
    {
        Task<ResponseDTO> CreateBloodRequestAsync(CreateBloodRequestDTO dto);
        Task<ResponseDTO> UpdateBloodRequestAsync(UpdateBloodRequestDTO dto);
        Task<ResponseDTO> DeleteBloodRequestAsync(Guid bloodRequestId);

        Task<ResponseDTO> GetAllBloodRequestsAsync();
        Task<ResponseDTO> GetBloodRequestByIdAsync(Guid bloodRequestId);
    }
}
