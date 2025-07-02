using BLL.Services.Interface;
using Common.DTO;
using Common.Enum;
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserMedical([FromBody] UpdateUserMedicalDTO updateUserMedicalDTO)
        {
            var response = await _userMedicalService.UpdateUserMedical(updateUserMedicalDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("check")]
        public async Task<IActionResult> CheckUserMedical()
        {
            var response = await _userMedicalService.CheckUserMedical();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllUserMedical()
        {
            var response = await _userMedicalService.GetAllUserMedical();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("change-status")]
        public async Task<IActionResult> ChangeStatus(Guid userMedicalId, MedicalType type)
        {
            var response = await _userMedicalService.ChangeStatus(userMedicalId, type);
            return StatusCode(response.StatusCode, response);
        }
    }
}