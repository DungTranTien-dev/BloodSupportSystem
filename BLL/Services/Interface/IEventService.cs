using Common.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(CreateEventDTO dto);
        Task<List<Event>> GetAllEventsAsync();
    }
}
