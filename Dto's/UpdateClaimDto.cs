namespace MedicalBillingApp.Dto_s
{
    public class UpdateClaimDto
    {
        public List<UpdateClaimStatus> claims { get; set; }

        public List<UpdateClaimLogDto> claimLogDtos { get; set; }
    }
}
