using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface ISeparatedBloodComponentService
    {
        Task<ResponseDTO> CreateSeparatedBloodComponentAsync(CreateSeparatedBloodComponentDTO createSeparatedBloodComponentDTO);
        Task<ResponseDTO> UpdateSeparatedBloodComponentAsync(UpdateSeparatedBloodComponentDTO updateSeparatedBloodComponentDTO);
        Task<ResponseDTO> DeleteSeparatedBloodComponentAsync(Guid SeparatedBloodComponentId);

        Task<ResponseDTO> GetAllSeparatedBloodComponentAsync();
        Task<ResponseDTO> GetSeparatedBloodComponentByIdAsync(Guid SeparatedBloodComponentId);
    }
}
