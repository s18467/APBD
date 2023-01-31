using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediDoc.Models;

[Table("Prescription")]
public partial class Prescription
{
    [Key]
    public int IdPrescription { get; set; }

    [Column(TypeName = "date")]
    public DateTime Date { get; set; }

    [Column(TypeName = "date")]
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    [ForeignKey("IdDoctor")]
    [InverseProperty("Prescriptions")]
    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    [ForeignKey("IdPatient")]
    [InverseProperty("Prescriptions")]
    public virtual Patient IdPatientNavigation { get; set; } = null!;

    [InverseProperty("IdPrescriptionNavigation")]
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; } = new List<PrescriptionMedicament>();
}
