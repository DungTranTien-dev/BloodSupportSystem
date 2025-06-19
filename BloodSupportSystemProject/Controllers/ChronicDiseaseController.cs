using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChronicDiseaseController : ControllerBase
    {
        private readonly IChronicDiseaseService _chronicDiseaseService;

        public ChronicDiseaseController(IChronicDiseaseService chronicDiseaseService)
        {
            _chronicDiseaseService = chronicDiseaseService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllChronicDiseases()
        {
            var result = await _chronicDiseaseService.GetAllChronicDisease();
            return StatusCode(result.StatusCode, result);
        }

        
    }
}
