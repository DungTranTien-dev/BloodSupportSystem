using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BloodSupportSystemProject.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            var result = await _userService.CreateUserAsync(createUserDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUserAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
           
            
            var result = await _userService.UpdateUserAsync(updateUserDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
