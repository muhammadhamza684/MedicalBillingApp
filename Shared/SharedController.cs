using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MedicalBillingApp.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private readonly ISharedService _sharedService;
        public SharedController(ISharedService sharedService)
        {
            _sharedService = sharedService; 
        }
        [HttpGet]

        public async Task<IActionResult> GetClaims([FromQuery] [Required]string status, int patientId)
        {
            var result = await _sharedService.GetClaimsWithStatus(status, patientId);
            return Ok(result);
        }
    }
}
