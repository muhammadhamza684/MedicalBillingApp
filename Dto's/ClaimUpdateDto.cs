namespace MedicalBillingApp.Dto_s
{
    public class ClaimUpdateDto
    {
        public ClaimDto claimDtos { get; set; }

        public List<ClaimLogDto> claimLogDtos { get; set; }
    }
}
