using BLL.Services.Interface;
using Common.DTO;
using DAL.Data;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Implement
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEventAsync(CreateEventDTO dto)
        {
            var newEvent = new Event
            {
                EventName = dto.EventName,
                EventDate = dto.EventDate,
                EventLocation = dto.EventLocation,
                Description = dto.Description,
                OrganizerId = dto.OrganizerId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return newEvent;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                                 .Include(e => e.Participants)
                                 .Include(e => e.Organizer)
                                 .ToListAsync();
        }
    }
}
