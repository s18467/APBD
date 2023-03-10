using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediDoc.Models;

[Table("Doctor")]
public partial class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [InverseProperty("IdDoctorNavigation")]
    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();
}
