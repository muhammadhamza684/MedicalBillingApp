using MedicalBillingApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicalBillingApp.AuthService
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("Secret");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryMinutes = jwtSettings.GetValue<int>("ExpiryInMinutes", 60);

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("JWT Secret is missing in configuration.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleName = user.Role != null ? user.Role.RoleName : "User";

            var claims = new List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Username),
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, roleName)
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}