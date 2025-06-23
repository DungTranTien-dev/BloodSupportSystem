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

        public AuthController(IAuthService authService)
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var respone = await _authService.Register(registerDTO);

            return StatusCode(respone.StatusCode, respone);
        }

        [HttpPost("google-signin")]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInDTO dto)
        {
            var result = await _authService.GoogleSignInAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("google-signup-complete")]
        public async Task<IActionResult> CompleteGoogleSignUp([FromBody] GoogleSignUpCompleteDTO dto)
        {
            var result = await _authService.CompleteGoogleSignUpAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
