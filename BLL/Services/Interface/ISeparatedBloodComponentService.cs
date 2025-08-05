using Common.DTO;
using Common.Enum;
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
        Task<ResponseDTO> AutoSeparateBloodComponentAsync(Guid BloodId);
        Task<ResponseDTO> UpdateSeparatedBloodComponentAsync(UpdateSeparatedBloodComponentDTO updateSeparatedBloodComponentDTO);
        Task<ResponseDTO> DeleteSeparatedBloodComponentAsync(Guid SeparatedBloodComponentId);

        Task<ResponseDTO> GetAllSeparatedBloodComponentAsync();
        Task<ResponseDTO> GetSeparatedBloodComponentByIdAsync(Guid SeparatedBloodComponentId);
        Task<bool> HasSufficientAvailableBloodComponentAsync(string bloodGroup, BloodComponentType componentType, double requiredVolume);
        Task<bool> SubtractBloodComponentVolumeAsync(string bloodGroup, BloodComponentType componentType, double requiredVolume);
    }

}
