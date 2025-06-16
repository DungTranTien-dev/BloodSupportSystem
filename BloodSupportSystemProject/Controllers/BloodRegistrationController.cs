using BLL.Services.Implement;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodRegistrationsController : ControllerBase
    {
        private readonly BloodRegistrationService _registrationService;

        public BloodRegistrationsController(BloodRegistrationService registrationService)
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

    }

}
