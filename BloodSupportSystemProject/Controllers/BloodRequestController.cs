using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodRequestController : ControllerBase
    {
        private readonly IBloodRequestService _bloodRequestService;

        public BloodRequestController(IBloodRequestService bloodRequestService)
        {
            _bloodRequestService = bloodRequestService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBloodRequest([FromBody] CreateBloodRequestDTO dto)
        {
            var result = await _bloodRequestService.CreateBloodRequestAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBloodRequests()
        {
            var result = await _bloodRequestService.GetAllBloodRequestsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodRequestById(Guid id)
        {
            var result = await _bloodRequestService.GetBloodRequestByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBloodRequest([FromBody] UpdateBloodRequestDTO dto)
        {
            var result = await _bloodRequestService.UpdateBloodRequestAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodRequest(Guid id)
        {
            var result = await _bloodRequestService.DeleteBloodRequestAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
