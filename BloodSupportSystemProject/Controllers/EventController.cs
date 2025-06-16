using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO createEventDTO)
        {
            var result = await _eventService.CreateEventAsync(createEventDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEventAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var result = await _eventService.GetEventByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDTO updateEventDTO)
        {
            var result = await _eventService.UpdateEventAsync(updateEventDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
