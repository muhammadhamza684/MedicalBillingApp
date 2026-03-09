using MedicalBillingApp.DAL;
using Microsoft.EntityFrameworkCore;
using Claim = MedicalBillingApp.Models.Claim;

namespace MedicalBillingApp.Shared
{
    public interface ISharedRepository
    {
        Task<List<Claim>> GetClaimsWithStatus(string status, int patientId);
    }
    public class SharedRepository : ISharedRepository   
    {
        private readonly MedicalBillingContext _context;
        public SharedRepository(MedicalBillingContext context)
        {
            _context = context;
        }

        public async Task<List<Claim>> GetClaimsWithStatus(string status, int patientId)
        {
            var claimResult = await _context.Claims.Where(x => x.ClaimStatus == status || x.PatientId == patientId)
                .ToListAsync();
            return claimResult;
        }
    }
}
