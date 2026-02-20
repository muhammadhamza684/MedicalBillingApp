using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalBillingApp.Dto_s
{
    public class CliamDto
    {
        public int ClaimId { get; set; }

        [StringLength(50)]
        public string ClaimNumber { get; set; } = null!;

        [StringLength(50)]
        public string ClaimStatus { get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        public int PatientId { get; set; }
    }
}
