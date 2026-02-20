using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalBillingApp.Models;

[Index("ClaimNumber", Name = "UQ__Claims__B7BA929C479C9D11", IsUnique = true)]
public partial class Claim
{
    [Key]
    public int ClaimId { get; set; }

    public int? AppointmentId { get; set; }

    [StringLength(50)]
    public string ClaimNumber { get; set; } = null!;

    [StringLength(50)]
    public string ClaimStatus { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public int PatientId { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("Claims")]
    public virtual Appointment Appointment { get; set; } = null!;

    [InverseProperty("Claim")]
    public virtual ICollection<ClaimLog> ClaimLogs { get; set; } = new List<ClaimLog>();

    [ForeignKey("PatientId")]
    [InverseProperty("Claims")]
    public virtual Patient Patient { get; set; } = null!;
}
