namespace MedicalBillingApp.Shared;

using MedicalBillingApp.Dto_s;
using MedicalBillingApp.HelperMethod;
using Claim = MedicalBillingApp.Models.Claim;

    public interface ISharedService
    {
        Task<ApiResponce<List<Claim>>> GetClaimsWithStatus(string status, int patientId);
    }
    public class SharedService : ISharedService
    {
        private readonly ISharedRepository _sharedRepository;
        public SharedService(ISharedRepository sharedRepository)
        {
            _sharedRepository = sharedRepository;
        }

    public async Task<ApiResponce<List<Claim>>> GetClaimsWithStatus(string status, int patientId)
    {
        try
        {
            var result = await _sharedRepository.GetClaimsWithStatus(status, patientId);
            int Count = result.Count;
            return ApiResponce<List<Claim>>.Success(result, Count);
        }
        catch (Exception ex)
        {
            return new ApiResponce<List<Claim>>(false, ex.Message, null);
        }
       
    }
}

