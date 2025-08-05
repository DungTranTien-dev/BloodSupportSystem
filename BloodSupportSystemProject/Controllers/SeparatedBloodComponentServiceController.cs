using BLL.Services.Implement;
using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeparatedBloodComponentController : ControllerBase
    {
        private readonly ISeparatedBloodComponentService _separatedBloodService;

        public SeparatedBloodComponentController(ISeparatedBloodComponentService separatedBloodService)
        {
            _separatedBloodService = separatedBloodService;
        }

        [HttpPost("autoseparateall/{bloodId}")]
        public async Task<IActionResult> SeparateBlood(Guid bloodId)
        {
            var result = await _separatedBloodService.AutoSeparateBloodComponentAsync(bloodId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateSeparatedBloodComponent([FromBody] CreateSeparatedBloodComponentDTO dto)
        {
            var result = await _separatedBloodService.CreateSeparatedBloodComponentAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSeparatedBloodComponents()
        {
            var result = await _separatedBloodService.GetAllSeparatedBloodComponentAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeparatedBloodComponentById(Guid id)
        {
            var result = await _separatedBloodService.GetSeparatedBloodComponentByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSeparatedBloodComponent([FromBody] UpdateSeparatedBloodComponentDTO dto)
        {
            var result = await _separatedBloodService.UpdateSeparatedBloodComponentAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeparatedBloodComponent(Guid id)
        {
            var result = await _separatedBloodService.DeleteSeparatedBloodComponentAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
