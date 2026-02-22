using MedicalBillingApp.Dto_s;
using MedicalBillingApp.Repository;

namespace MedicalBillingApp.Services
{
    public interface IUserService
    {
        Task<UserRegistrationDtos> userRegistration(UserRegistrationDtos userDtoS);

        Task<bool> loginUser(UserLoginDto userDto);

        Task<ClaimCompositionDto> InsertClaims(ClaimCompositionDto claimCompositionDto);

        Task<bool> UpdateClaim(ClaimCompositionDto claimCompositionDto);

        Task<PatientClaimAndAppionmentDto> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto);
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

        public async Task<PatientClaimAndAppionmentDto> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto)
        {
            var result = await _userRepository.CreateClaimAndAppionment(patientClaimAndAppionmentDto);
            return result;  
        }
    }
}
