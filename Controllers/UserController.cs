using MedicalBillingApp.Dto_s;
using MedicalBillingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace MedicalBillingApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserRegistrationDtos userDto)
        {
            var result = await _userService.userRegistration(userDto);
            return Ok(result);  
        }

        [HttpPost]

        public async Task<IActionResult> userLogin(UserLoginDto userLoginDto)
        {
            var result = await _userService.loginUser(userLoginDto);
            return Ok($"user login Successfulll"+ result);
        }

        [HttpPost]

        public async Task<IActionResult> InserClaims(ClaimCompositionDto claimCompositionDto)
        {
            var Result = await _userService.InsertClaims(claimCompositionDto);
            return Ok(Result);  
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClaim(ClaimCompositionDto claimCompositionDto)
        {
            var result = await _userService.UpdateClaim(claimCompositionDto);
            return Ok(result);
        }

        [HttpPost]
         public async Task<IActionResult> BookAppionmentWithClaim([FromBody]PatientClaimAndAppionmentDto patientClaimAndAppionmentDto)
        {
            var result = await _userService.CreateClaimAndAppionment(patientClaimAndAppionmentDto);
            return Ok(result);
        }

    }
}
