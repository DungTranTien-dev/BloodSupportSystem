using BLL.DTOs;
using BLL.Services.Interface;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Example GET method for CreatedAtAction
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            // You can implement this as needed
            return Ok(); // Placeholder
        }
    }
}