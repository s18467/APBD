using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediDoc.Models;

[PrimaryKey("IdMedicament", "IdPrescription")]
[Table("Prescription_Medicament")]
public partial class PrescriptionMedicament
{
    [Key]
    public int IdMedicament { get; set; }

    [Key]
    public int IdPrescription { get; set; }

    public int Dose { get; set; }

    [StringLength(100)]
    public string Details { get; set; } = null!;

    [ForeignKey("IdMedicament")]
    [InverseProperty("PrescriptionMedicaments")]
    public virtual Medicament IdMedicamentNavigation { get; set; } = null!;

    [ForeignKey("IdPrescription")]
    [InverseProperty("PrescriptionMedicaments")]
    public virtual Prescription IdPrescriptionNavigation { get; set; } = null!;
}
