using MediDoc.Jwt.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace MediDoc.Jwt;

public class MediContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public MediContext()
    {
    }

    public MediContext(DbContextOptions<MediContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddKeysAndConstraints(modelBuilder);
        Seed(modelBuilder);
    }

    private static void AddKeysAndConstraints(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity => { entity.HasKey(e => e.IdDoctor).HasName("Doctor_pk"); });

        modelBuilder.Entity<Medicament>(entity => { entity.HasKey(e => e.IdMedicament).HasName("Medicament_pk"); });

        modelBuilder.Entity<Patient>(entity => { entity.HasKey(e => e.IdPatient).HasName("Patient_pk"); });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.IdPrescription).HasName("Prescription_pk");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Patient");
        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("Prescription_Medicament_pk");

            entity.HasOne(d => d.IdMedicamentNavigation).WithMany(p => p.PrescriptionMedicaments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Medicament_Medicament");

            entity.HasOne(d => d.IdPrescriptionNavigation).WithMany(p => p.PrescriptionMedicaments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Medicament_Prescription");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pk");
        });
    }

    private void Seed(ModelBuilder mb)
    {
        #region Account
        var password = "mzalpqw";
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        var saltBase64 = Convert.ToBase64String(salt);

        var user = new Account()
        {
            Id = 1,
            Username = "Admin",
            Email = "admin@test.pl",
            Password = hashed,
            Salt = saltBase64,
            RefreshToken = null,
            RefreshTokenExp = null
        };
        mb.Entity<Account>().HasData(user);
        #endregion
        #region Doctor
        mb.Entity<Doctor>().HasData(
            new Doctor
            {
                IdDoctor = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan@example.com"
            },
            new Doctor
            {
                IdDoctor = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna@example.com"
            },
            new Doctor
            {
                IdDoctor = 3,
                FirstName = "Piotr",
                LastName = "Kozłowski",
                Email = "piotr@example.com"
            }
        );
        #endregion
        #region Patient
        mb.Entity<Patient>().HasData(
            new Patient
            {
                IdPatient = 1,
                FirstName = "Adam",
                LastName = "Niwiński",
                Birthdate = new DateTime(1990, 1, 1)
            },
            new Patient
            {
                IdPatient = 2,
                FirstName = "Julia",
                LastName = "Wiśniewska",
                Birthdate = new DateTime(1985, 12, 1)
            },
            new Patient
            {
                IdPatient = 3,
                FirstName = "Tomasz",
                LastName = "Wojciechowski",
                Birthdate = new DateTime(1980, 6, 15)
            }
        );
        #endregion
        #region Medicament
        mb.Entity<Medicament>().HasData(
            new Medicament
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Przeciwbólowy",
                Type = "Tabletki"
            },
            new Medicament
            {
                IdMedicament = 2,
                Name = "Ibuprofen",
                Description = "Przeciwbólowy i przeciwzapalny",
                Type = "Tabletki"
            },
            new Medicament
            {
                IdMedicament = 3,
                Name = "Ketoprofen",
                Description = "Przeciwbólowy i przeciwzapalny",
                Type = "Żel"
            }
        );
        #endregion
        #region Prescription
        mb.Entity<Prescription>().HasData(
            new Prescription
            {
                IdPrescription = 1,
                Date = new DateTime(2021, 1, 1),
                DueDate = new DateTime(2021, 1, 31),
                IdDoctor = 1,
                IdPatient = 1
            },
            new Prescription
            {
                IdPrescription = 2,
                Date = new DateTime(2021, 2, 15),
                DueDate = new DateTime(2021, 3, 15),
                IdDoctor = 2,
                IdPatient = 2
            },
            new Prescription
            {
                IdPrescription = 3,
                Date = new DateTime(2021, 3, 1),
                DueDate = new DateTime(2021, 3, 31),
                IdDoctor = 3,
                IdPatient = 3
            }
        );
        #endregion
        #region PrescriptionMedicament
        mb.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 1,
                Details = "Rano"
            },
            new PrescriptionMedicament
            {
                IdMedicament = 2,
                IdPrescription = 2,
                Dose = 2,
                Details = "Wieczorem"
            },
            new PrescriptionMedicament
            {
                IdMedicament = 3,
                IdPrescription = 3,
                Dose = 3,
                Details = "3 razy dziennie"
            }
        );
        #endregion
    }
}
