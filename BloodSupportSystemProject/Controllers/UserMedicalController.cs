using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using static Common.DTO.AuthDTO;

namespace BloodSupportSystemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMedicalController : ControllerBase
    {
        private readonly IUserMedicalService _userMedicalService;

        public UserMedicalController(IUserMedicalService userMedicalService)
        {
            _userMedicalService = userMedicalService;
        }

        ///<summary>
        ///login
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUserMedical([FromBody] CreateUserMediCalDTO createUserMediCalDTO)
        {
            var respone = await _userMedicalService.CreateUserMedical(createUserMediCalDTO);

            return StatusCode(respone.StatusCode, respone);
        }
    }
}