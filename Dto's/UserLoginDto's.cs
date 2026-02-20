using System.ComponentModel.DataAnnotations;

namespace MedicalBillingApp.Dto_s
{
    public class UserLoginDto
    {
        [StringLength(150)]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}
