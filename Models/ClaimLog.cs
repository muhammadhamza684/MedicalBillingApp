using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalBillingApp.Models;

public partial class ClaimLog
{
    [Key]
    public int ClaimLogId { get; set; }

    public int ClaimId { get; set; }

    [StringLength(500)]
    public string LogMessage { get; set; } = null!;

    [StringLength(50)]
    public string? OldStatus { get; set; }

    [StringLength(50)]
    public string? NewStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LoggedDate { get; set; }

    [ForeignKey("ClaimId")]
    [InverseProperty("ClaimLogs")]
    public virtual Claim Claim { get; set; } = null!;
}
