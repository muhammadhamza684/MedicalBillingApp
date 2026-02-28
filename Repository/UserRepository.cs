using MedicalBillingApp.DAL;
using MedicalBillingApp.Dto_s;
using MedicalBillingApp.HelperMethod;
using MedicalBillingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace MedicalBillingApp.Repository
{
    public interface IUserRepository
    {
        Task<UserRegistrationDtos> UserRegistration(UserRegistrationDtos userDto);

        Task<bool> loginUser(UserLoginDto userDto);

        Task<ClaimCompositionDto> InsertClaims(ClaimCompositionDto claimCompositionDto);

        Task<bool> UpdateClaim(ClaimCompositionDto claimCompositionDto);

        Task<PatientClaimAndAppionmentDto> CreateClaimAndAppionment(PatientClaimAndAppionmentDto patientClaimAndAppionmentDto);

    }
    public class UserRepository : IUserRepository
    {
        private readonly MedicalBillingContext _context;
        private readonly IPasswordHelper _passwordHelper;
        public UserRepository(MedicalBillingContext context, IPasswordHelper passwordHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
        }



        public async Task<UserRegistrationDtos> UserRegistration(UserRegistrationDtos userDtoS)
        {
            var user = new User();
            user.Username = userDtoS.Username;
            user.Email = userDtoS.Email;

            // Hash the password
            user.PasswordHash = _passwordHelper.HashPassword(user, userDtoS.PasswordHash);

            // Save to DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Clear password before returning
            userDtoS.PasswordHash = null;
            return userDtoS;
        }

        public async Task<bool> loginUser(UserLoginDto userDto)
        {
            // 1️⃣ Find user by email first
            var userResult = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == userDto.Email);

            // 2️⃣ Agar user nahi mila to false return karo
            if (userResult == null)
                return false;

            // 3️⃣ Password verify karo using PasswordHelper
            bool isPasswordValid = _passwordHelper.VerifyPassword(userResult, userResult.PasswordHash, userDto.Password);

            return isPasswordValid;
        }

        public async Task<ClaimCompositionDto> InsertClaims(ClaimCompositionDto dto)
        {
            // 1️⃣ Insert Patient (single)
            var patientDto = dto.patientDtos.First();

            var patient = new Patient
            {
                //UserId = patientDto.UserId,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Phone = patientDto.Phone,
                CreatedDate = DateTime.Now
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();   // 👈 PatientId generate hoga

            // 2️⃣ Insert Claims (multiple possible)
            var claims = dto.cliamDtos.Select(x => new Claim
            {
                PatientId = patient.PatientId,   // 🔑 FK
               // UserId = patient.UserId,          // 🔑 FK
                ClaimNumber = x.ClaimNumber,
                ClaimStatus = x.ClaimStatus,
                TotalAmount = x.TotalAmount,
                CreatedDate = DateTime.Now
            }).ToList();

            _context.Claims.AddRange(claims);
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> UpdateClaim(ClaimCompositionDto claimCompositionDto)
        {
            // 1️⃣ Patient update
            var patientDto = claimCompositionDto.patientDtos.First();

            var patient = await _context.Patients
                .FirstOrDefaultAsync(x => x.PatientId == patientDto.PatientId);

            if (patient == null)
                throw new Exception("Patient not found");

       
            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.Phone = patientDto.Phone;
            patient.Gender = patientDto.Gender;
            patient.DateOfBirth = patientDto.DateOfBirth;

           
            _context.Entry(patient).State = EntityState.Modified;


            foreach (var claimDto in claimCompositionDto.cliamDtos)
            {
                var claim = await _context.Claims
                    .FirstOrDefaultAsync(c => c.ClaimId == claimDto.ClaimId);

                if (claim != null)
                {
                    claim.ClaimNumber = claimDto.ClaimNumber;
                    claim.ClaimStatus = claimDto.ClaimStatus;
                    claim.TotalAmount = claimDto.TotalAmount;
                    claim.CreatedDate = claimDto.CreatedDate;


                    _context.Entry(claim).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PatientClaimAndAppionmentDto> CreateClaimAndAppionment(
     PatientClaimAndAppionmentDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
              
                var patientDto = dto.PatientDtos.First();

                var patient = new Patient
                {
                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    Gender = patientDto.Gender,
                    Phone = patientDto.Phone,
                    CreatedDate = DateTime.Now
                };

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();


                var doctorDto = dto.DoctorDtos.First();

                var doctor = await _context.Doctors
                    .FirstOrDefaultAsync(x => x.DoctorId == doctorDto.DoctorId);

                if (doctor == null)
                {
                    doctor = new Doctor
                    {
                        DoctorName = doctorDto.DoctorName,
                        Specialty = doctorDto.Specialty,
                        CreatedDate = DateTime.Now
                    };

                    _context.Doctors.Add(doctor);
                    await _context.SaveChangesAsync();
                }

                var appointmentDto = dto.appointments.First();

                var appointment = new Appointment
                {
                    PatientId = patient.PatientId,
                    DoctorId = doctor.DoctorId,
                    VisitDate = appointmentDto.VisitDate == default
                                    ? DateTime.Now
                                    : appointmentDto.VisitDate,
                    Status = appointmentDto.Status
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();


             
                var claims = dto.claims.Select(x => new Claim
                {
                    ClaimNumber = Guid.NewGuid().ToString().Substring(0,5), // ✅ UNIQUE
                    ClaimStatus = x.ClaimStatus,
                    TotalAmount = x.TotalAmount,
                    CreatedDate = DateTime.Now,
                    PatientId = patient.PatientId,
                    AppointmentId = appointment.AppointmentId
                }).ToList();

                _context.Claims.AddRange(claims);
                await _context.SaveChangesAsync();


                await transaction.CommitAsync();

                return dto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
