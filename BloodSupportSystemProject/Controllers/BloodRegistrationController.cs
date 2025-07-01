using BLL.Services.Implement;
using BLL.Services.Interface;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodRegistrationsController : ControllerBase
    {
        private readonly IBloodRegistrationService _registrationService;

        public BloodRegistrationsController(IBloodRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("{eventId}")]
        public async Task<IActionResult> Register(Guid eventId)
        {
            var result = await _registrationService.CreateByEvenId(eventId);
            return Ok(result);
        }

        [HttpPost("change-status/{eventId}")]
        public async Task<IActionResult> ChangeStatus(RegisterType type ,Guid eventId)
        {
            var result = await _registrationService.UpdateStatus(type ,eventId);
            return Ok(result);
        }

        [HttpGet("user")] 
        public async Task<IActionResult> GetRegistrationById()
        {
            var result = await _registrationService.GetByUserId();
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllRegistrations()
        {
            var result = await _registrationService.GetAll();
            return Ok(result);
        }
    }

}
