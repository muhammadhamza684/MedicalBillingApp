using MedicalBillingApp.AuthService;
using MedicalBillingApp.Dto_s;
using MedicalBillingApp.HelperMethod;
using MedicalBillingApp.Models;
using MedicalBillingApp.Repository;

namespace MedicalBillingApp.Services
{
    public interface IUserService
    {
        Task<UserRegistrationDtos> userRegistration(UserRegistrationDtos userDtoS);

        Task<string>? loginUser(UserLoginDto userDto);

        Task<ClaimCompositionDto> InsertClaims(ClaimCompositionDto claimCompositionDto);

        Task<bool> UpdateClaim(ClaimCompositionDto claimCompositionDto);

        Task<ApiResponce<PatientClaimAndAppionmentDto>> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto);

        Task<ApiResponce<UpdateClaimDto>> UpdateClaims(UpdateClaimDto dto, int claimId);
    }

    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IAuthService authService )
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<UserRegistrationDtos> userRegistration(UserRegistrationDtos userDtoS)
        {
            var result = await _userRepository.UserRegistration(userDtoS);

  
            return result;
        }

      

        public async Task<ClaimCompositionDto> InsertClaims(ClaimCompositionDto claimCompositionDto)
        {
            var result = await _userRepository.InsertClaims(claimCompositionDto);
            return result;
                
        }

        public async Task<bool> UpdateClaim(ClaimCompositionDto claimCompositionDto)
        {
            var result = await _userRepository.UpdateClaim(claimCompositionDto);
            return true;
        }

        public async Task<ApiResponce<PatientClaimAndAppionmentDto>> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto)
        {
            try
            {
                var result = await _userRepository.CreateClaimAndAppionment(patientClaimAndAppionmentDto);

                return ApiResponce<PatientClaimAndAppionmentDto>.Success(result, default);
            }
            catch (Exception ex)
            {
                return new ApiResponce<PatientClaimAndAppionmentDto>(false, ex.Message, null);
            }
        }

        public async Task<ApiResponce<UpdateClaimDto>> UpdateClaims(UpdateClaimDto dto, int claimId)
        {
            try
            {
                var result = await _userRepository.UpdateClaims(dto, claimId);
                return ApiResponce<UpdateClaimDto>.Success(result);
            }
            catch (Exception ex)
            {

                return new ApiResponce<UpdateClaimDto>(false, ex.Message, null);
            }
        }

        public async Task<string>? loginUser(UserLoginDto userDto)
        {
           
            var user = await _userRepository.loginUser(userDto);

            if (user == null)
                return null;  

            
            var token = _authService.GenerateJwtToken(user);

            return token;
        }
    }
}
