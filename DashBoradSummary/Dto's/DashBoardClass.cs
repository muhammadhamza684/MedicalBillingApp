namespace MedicalBillingApp.DashBoradSummary.Dto_s
{
    public class DashBoardClass
    {
        public int? totalClaims { get; set; }

        public int? totalPatient { get; set; }

        public int? pendingClaim { get; set; }

        public int? approvedClaim { get; set; } 

        public decimal? sum { get; set; }
    }
}
