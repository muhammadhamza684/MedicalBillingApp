using MedicalBillingApp.DashBoradSummary.Dto_s;
using MedicalBillingApp.DashBoradSummary.Repository;
using MedicalBillingApp.Models;

//using System.Security.Claims;



namespace MedicalBillingApp.DashBoradSummary.Service
{
    public interface IDashboardServices
    {
        Task<DashBoardClass> GetDashBoardData();

        Task<List<Claim>> GetAllClaims();
}
public class DashboardServices : IDashboardServices
    {
        private readonly IDashBoradRepository _repository;
        public DashboardServices(IDashBoradRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Claim>> GetAllClaims()
        {
            var result = await _repository.GetAllClaims();

            return result;

            
        }

        public async Task<DashBoardClass> GetDashBoardData()
        {
            var claims = await _repository.GetAllClaims();

            var totalClaims = claims.Count();

            var Sum = claims.Sum(x=>x.TotalAmount); 

            var patients = await _repository.GetAllPatients();

            var totalPatients = patients.Count();

            var pending = await _repository.GetAllPendingClaims();

            var totalPending = pending.Count();

            var approved = await _repository.GetAllApprovedClaims();

            var totalApproved = approved.Count();

            return new DashBoardClass
            {
                totalClaims = totalClaims,
                totalPatient = totalPatients,
                pendingClaim = totalPending,
                approvedClaim = totalApproved,
                sum = Sum,
            };

        }
    }
}
