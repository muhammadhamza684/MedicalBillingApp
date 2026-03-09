using MedicalBillingApp.DAL;
using MedicalBillingApp.Models;
using Microsoft.AspNetCore.Identity;

namespace MedicalBillingApp.HelperMethod
{
    public interface IPasswordHelper
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string password);
    }

    public class PasswordHelper : IPasswordHelper
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHelper(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public string HashPassword(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be empty", nameof(password));

            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedPassword, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(hashedPassword)) return false; // Prevents FormatException
            if (string.IsNullOrWhiteSpace(password)) return false;

            try
            {
                var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
                return result == PasswordVerificationResult.Success;
            }
            catch
            {
                // Agar hashedPassword invalid format hai, safe return false
                return false;
            }
        }
    }
}