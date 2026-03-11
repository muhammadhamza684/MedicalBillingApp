using MedicalBillingApp.DashBoradSummary.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBillingApp.DashBoradSummary.Controler
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardServices _services;
        public DashboardController(IDashboardServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashBoardDetails()
        {
            var result = await _services.GetDashBoardData();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClaimStatusInformation()
        {
            var result = await _services.GetClaimStatusInformation();
            return Ok(result);
        }
    }
}
