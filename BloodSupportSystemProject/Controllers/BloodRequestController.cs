using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodRequestController : ControllerBase
    {
        private readonly IBloodRequestService _service;

        public BloodRequestController(IBloodRequestService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var data = await _service.GetByUserAsync(userId);
            return Ok(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBloodRequestDTO dto)
        {
            try
            {
                var success = await _service.CreateAsync(dto);
                return success
                    ? Ok("Tạo yêu cầu thành công")
                    : BadRequest("Tạo yêu cầu thất bại");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateBloodRequestDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result ? Ok("Cập nhật thành công") : NotFound("Không tìm thấy");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok("Xoá thành công") : NotFound("Không tìm thấy");
        }
    }

}
