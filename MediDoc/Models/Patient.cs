using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediDoc.Models;

[Table("Patient")]
public partial class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime Birthdate { get; set; }

    [InverseProperty("IdPatientNavigation")]
    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();
}
