using MedicalBillingApp.Models;

namespace MedicalBillingApp.Dto_s
{
    public class PatientClaimAndAppionmentDto
    {
        public List<PatientDto> PatientDtos { get; set; }

        public List<ClaimDto> claims { get; set; }

        public List<DoctorDto> DoctorDtos { get; set; }

        public List<AppionmentDto> appointments { get; set; }
        
    }
}
