using MedicalBillingApp.Dto_s;
using MedicalBillingApp.HelperMethod;
using MedicalBillingApp.Repository;

namespace MedicalBillingApp.Services
{
    public interface IUserService
    {
        Task<UserRegistrationDtos> userRegistration(UserRegistrationDtos userDtoS);

        Task<bool> loginUser(UserLoginDto userDto);

        Task<ClaimCompositionDto> InsertClaims(ClaimCompositionDto claimCompositionDto);

        Task<bool> UpdateClaim(ClaimCompositionDto claimCompositionDto);

        Task<ApiResponce<PatientClaimAndAppionmentDto>> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserRegistrationDtos> userRegistration(UserRegistrationDtos userDtoS)
        {
            var result = await _userRepository.UserRegistration(userDtoS);
            return result;
        }

        public async Task<bool> loginUser(UserLoginDto userDto)
        {
            var result = await _userRepository.loginUser(userDto);
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
            return result;
        }

        public async Task<ApiResponce<PatientClaimAndAppionmentDto>> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto)
        {
            try
            {
                var result = await _userRepository.CreateClaimAndAppionment(patientClaimAndAppionmentDto);

                return ApiResponce<PatientClaimAndAppionmentDto>.Success(result, "Claim and Appointment Created Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponce<PatientClaimAndAppionmentDto>(false, ex.Message, null);
            }
        }
    }
}
