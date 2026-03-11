using MedicalBillingApp.DAL;
using MedicalBillingApp.DashBoradSummary.Dto_s;
using MedicalBillingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalBillingApp.DashBoradSummary.Repository
{
    public interface IDashBoradRepository
    {
        Task<List<Patient>> GetAllPatients();

        Task<List<Claim>> GetAllClaims();

        Task<List<Claim>> GetAllPendingClaims();

        Task<List<Claim>> GetAllApprovedClaims();

        Task<List<ClaimsByStatusDto>> GetClaimStatusInformation();
    }
    public class DashBoradRepository : IDashBoradRepository
    {

        public const string PendingStatus = "Pending";
        public const string ApprovedStatus = "Approved";
        private readonly MedicalBillingContext _context;
        public DashBoradRepository(MedicalBillingContext context)
        {
            _context = context;
        }

        public async Task<List<Claim>> GetAllClaims()
        {
            var result = await _context.Claims.ToListAsync();   
            return result;
        }


        public async Task<List<Patient>> GetAllPatients()
        {
            var result = await _context.Patients.ToListAsync();
            return result;
        }

        public async Task<List<Claim>> GetAllPendingClaims()
        {
            var result = await _context.Claims.Where(x=>x.ClaimStatus == PendingStatus).ToListAsync();

            return result;

        }

        public async Task<List<Claim>> GetAllApprovedClaims()
        {
            var result = await _context.Claims.Where(x => x.ClaimStatus == ApprovedStatus).ToListAsync();

            return result;
        }

        public async Task<List<ClaimsByStatusDto>> GetClaimStatusInformation()
        {
            var result = await _context.Set<ClaimsByStatusDto>().FromSqlInterpolated($"Exec sp_claimStatusCount").ToListAsync();

            return result;
        }
    }
}
