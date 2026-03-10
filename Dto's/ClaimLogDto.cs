using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalBillingApp.Dto_s
{
    public class ClaimLogDto
    {
        public int ClaimLogId { get; set; }

        public int ClaimId { get; set; }

        [StringLength(500)]
        public string LogMessage { get; set; } = null!;

        [StringLength(50)]
        public string? OldStatus { get; set; }

        [StringLength(50)]
        public string? NewStatus { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LoggedDate { get; set; }
    }
}
