using BLL.Services.Interface;
using Common.DTO;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodController : ControllerBase
    {
        private readonly IBloodService _bloodService;

        public BloodController(IBloodService bloodService)
        {
            _bloodService = bloodService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateBloodDTO dto)
        //{
        //    try
        //    {
        //        var createdBlood = await _bloodService.CreateBloodAsync(dto);
        //        return CreatedAtAction(nameof(GetById), new { id = createdBlood.BloodId }, createdBlood);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bloodService.GetAllBloodsAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _bloodService.GetBloodByIdAsync(id);

            return Ok(response);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBloodDTO dto)
        {
           var response = await _bloodService.UpdateBloodAsync(id, dto);
           
            return Ok(response);
        }

        [HttpPost("change-status")]
        public async Task<IActionResult> ChangeStatus(Guid id, BloodSeparationStatus status)
        {
            var response = await _bloodService.ChangeStatus(id, status);
            return Ok(response);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        var result = await _bloodService.DeleteBloodAsync(id);
        //        if (!result)
        //            return NotFound();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        //    }
        //}
    }

}
