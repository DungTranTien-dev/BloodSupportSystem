using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Common.DTO.AuthDTO;

namespace BloodSupportSystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }

        ///<summary>
        ///login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var respone = await _authService.Login(loginDTO);

            return StatusCode(respone.StatusCode, respone);
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInDTO dto)
        {
            var response = await _authService.GoogleSignInAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("google/complete")]
        public async Task<IActionResult> CompleteGoogleSignUp([FromBody] GoogleSignUpCompleteDTO dto)
        {
            var response = await _authService.CompleteGoogleSignUpAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
