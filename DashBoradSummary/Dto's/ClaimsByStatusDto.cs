using Microsoft.Identity.Client;

namespace MedicalBillingApp.DashBoradSummary.Dto_s
{
    public class ClaimsByStatusDto
    {
        public string ClaimStatus { get; set; } 

        public int Count { get; set; }  
    }
}
