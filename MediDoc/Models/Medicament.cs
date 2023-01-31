using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediDoc.Models;

[Table("Medicament")]
public partial class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [StringLength(100)]
    public string Type { get; set; } = null!;

    [InverseProperty("IdMedicamentNavigation")]
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; } = new List<PrescriptionMedicament>();
}
