using BLL.Services.Interface;
using Common.DTO;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> CreateEventAsync(CreateEventDTO createEventDTO)
        {
            if (string.IsNullOrWhiteSpace(createEventDTO.Title))
                return new ResponseDTO("Title is required.", 400, false);

            if (string.IsNullOrWhiteSpace(createEventDTO.Location))
                return new ResponseDTO("Location is required.", 400, false);

            var newEvent = new DonationEvent
            {
                DonationEventId = Guid.NewGuid(),
                Title = createEventDTO.Title,
                Location = createEventDTO.Location,
                StartTime = createEventDTO.StartTime,
                EndTime = createEventDTO.EndTime,
                Description = createEventDTO.Description
            };

            await _unitOfWork.EventRepo.AddAsync(newEvent);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Create event successful.", 200, true);
        }

        public async Task<ResponseDTO> DeleteEventAsync(Guid eventId)
        {
            var existingEvent = await _unitOfWork.EventRepo.GetByIdAsync(eventId);
            if (existingEvent == null)
                return new ResponseDTO("Event not found.", 400, false);

            await _unitOfWork.EventRepo.DeleteAsync(eventId);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Delete event successful.", 200, true);
        }

        public async Task<ResponseDTO> GetAllEventAsync()
        {
            var allEvents = _unitOfWork.EventRepo.GetAll();
            if (allEvents == null || !allEvents.Any())
                return new ResponseDTO("No events found.", 400, false);

            var eventDTOs = allEvents.Select(e => new
            {
                e.DonationEventId,
                e.Title,
                e.Location,
                e.StartTime,
                e.EndTime,
                e.Description
            });

            return new ResponseDTO("Events retrieved successfully.", 200, true, eventDTOs);
        }

        public async Task<ResponseDTO> GetEventByIdAsync(Guid eventId)
        {
            var eventItem = await _unitOfWork.EventRepo.GetByIdAsync(eventId);
            if (eventItem == null)
                return new ResponseDTO("Event not found.", 404, false);

            var eventDTO = new
            {
                eventItem.DonationEventId,
                eventItem.Title,
                eventItem.Location,
                eventItem.StartTime,
                eventItem.EndTime,
                eventItem.Description
            };

            return new ResponseDTO("Event retrieved successfully.", 200, true, eventDTO);
        }

        public async Task<ResponseDTO> UpdateEventAsync(UpdateEventDTO updateEventDTO)
        {
            var eventToUpdate = await _unitOfWork.EventRepo.GetByIdAsync(updateEventDTO.DonationEventId);
            if (eventToUpdate == null)
                return new ResponseDTO("Event not found.", 404, false);

            eventToUpdate.Title = updateEventDTO.Title;
            eventToUpdate.Location = updateEventDTO.Location;
            eventToUpdate.StartTime = updateEventDTO.StartTime;
            eventToUpdate.EndTime = updateEventDTO.EndTime;
            eventToUpdate.Description = updateEventDTO.Description;

            await _unitOfWork.EventRepo.UpdateAsync(eventToUpdate);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Update event successful.", 200, true);
        }
    }

    
    
}
