using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using static Common.DTO.AuthDTO;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMedicalController : ControllerBase
    {
        private readonly IUserMedicalService _service;

        public UserMedicalController(IUserMedicalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserMedicalDTO dto)
        {
            var result = await _service.AddAsync(dto);
            return result ? Ok("Created") : BadRequest("Failed to create");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserMedicalDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            return result ? Ok("Updated") : NotFound("Not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok("Deleted") : NotFound("Not found");
        }
    }
}