using System.ComponentModel.DataAnnotations;

namespace MedicalBillingApp.Dto_s
{
    public class UserRegistrationDtos
    {
        public int UserId { get; set; }

        [StringLength(100)]
        public string Username { get; set; } = null!;

        [StringLength(500)]
        public string PasswordHash { get; set; } = null!;

        [StringLength(150)]
        public string? Email { get; set; }

        public bool? IsActive { get; set; }
    }
}
