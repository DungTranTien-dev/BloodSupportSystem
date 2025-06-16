using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEventService
    {
        Task<ResponseDTO> CreateEventAsync(CreateEventDTO createEventDTO);
        Task<ResponseDTO> UpdateEventAsync(UpdateEventDTO updateEventDTO);
        Task<ResponseDTO> DeleteEventAsync(Guid userId);

        Task<ResponseDTO> GetAllEventAsync();
        Task<ResponseDTO> GetEventByIdAsync(Guid userId);
    }
}
