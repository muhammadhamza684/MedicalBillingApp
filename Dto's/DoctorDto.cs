using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalBillingApp.Dto_s
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }

        public int? UserId { get; set; }

        [StringLength(150)]
        public string DoctorName { get; set; } = null!;

        [StringLength(100)]
        public string? Specialty { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
