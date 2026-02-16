using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalBillingApp.Models;

public partial class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    public int? UserId { get; set; }

    [StringLength(150)]
    public string DoctorName { get; set; } = null!;

    [StringLength(100)]
    public string? Specialty { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("UserId")]
    [InverseProperty("Doctors")]
    public virtual User? User { get; set; }
}
