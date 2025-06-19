using BLL.Services.Interface;
using Common.DTO;
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
            try
            {
                var bloods = await _bloodService.GetAllBloodsAsync();
                return Ok(bloods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var blood = await _bloodService.GetBloodByIdAsync(id);
                if (blood == null)
                    return NotFound();
                return Ok(blood);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBloodDTO dto)
        {
            try
            {
                var updatedBlood = await _bloodService.UpdateBloodAsync(id, dto);
                if (updatedBlood == null)
                    return NotFound();

                return Ok(updatedBlood);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _bloodService.DeleteBloodAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }
    }
}
