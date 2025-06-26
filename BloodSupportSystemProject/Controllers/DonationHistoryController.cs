using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationHistoryController : ControllerBase
    {
        private readonly IDonationHistoryService _service;

        public DonationHistoryController(IDonationHistoryService service)
        {
            _service = service;
        }

        // GET: /api/DonationHistory/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDonationHistoryByUserId(Guid userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            if (result == null || result.Count == 0)
            {
                return NotFound("Không có lịch sử hiến máu nào cho người dùng này.");
            }

            return Ok(result);
        }
    }
}
