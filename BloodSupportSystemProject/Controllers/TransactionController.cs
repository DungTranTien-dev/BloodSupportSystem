using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        // Temporarily commented out due to missing IVnPayService
        // private readonly IVnPayService _vnPayService;

        public TransactionController(/* IVnPayService vnPayService */)
        {
            // _vnPayService = vnPayService;
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        //{
        //    var result = await _userService.CreateUserAsync(createUserDTO);
        //    return StatusCode(result.StatusCode, result);
        //}

        [HttpPost("callback")]
        public IActionResult CallBackVNPay([FromQuery] Guid transactionId, [FromQuery] Guid bloodRequestId, [FromQuery] string status)
        {
            // Temporarily disabled due to missing IVnPayService
            // var result = await _vnPayService.CallBackVNPay(transactionId, bloodRequestId, status);
            // return StatusCode(result.StatusCode, result);
            return Ok(new { message = "VNPay service temporarily disabled" });
        }
    }
}
