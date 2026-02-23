using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalBillingApp.Dto_s
{
    public class AppionmentDto
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime VisitDate { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }
    }
}
