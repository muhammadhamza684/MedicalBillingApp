using MedicalBillingApp.AuthService;
using MedicalBillingApp.DAL;
using MedicalBillingApp.HelperMethod;
using MedicalBillingApp.Models;
using MedicalBillingApp.Repository;
using MedicalBillingApp.Services;
using MedicalBillingApp.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------
// Database
// ------------------------
builder.Services.AddDbContext<MedicalBillingContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MedicalBillingDB")
    ));

// ------------------------
// JWT Settings
// ------------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("Secret");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");

if (string.IsNullOrEmpty(secretKey))
    throw new Exception("JWT Secret missing in configuration!");

// ------------------------
// Repositories & Services
// ------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISharedRepository, SharedRepository>();
builder.Services.AddScoped<ISharedService, SharedService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Password hashing (optional, can remove if you want plain passwords)
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// ------------------------
// JWT Authentication
// ------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// ------------------------
// Build app
// ------------------------
var app = builder.Build();

// ------------------------
// Middleware
// ------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   // <-- Must be before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();