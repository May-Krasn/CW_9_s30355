using Hospital_CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_CodeFirst.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctor = new List<Doctor>
        {
            new()
            {
                IdDoctor = 1,
                FirstName = "Mel",
                LastName = "Medarda",
                Email = "melmedarda@gmail.com"
            },
            new()
            {
                IdDoctor = 2,
                FirstName = "Ambessa",
                LastName = "Medarda",
                Email = "ambessamedarda@gmail.com"
            }
        };
        
        var patients = new List<Patient>
        {
            new()
            {
                IdPatient = 1,
                FirstName = "Vander",
                LastName = "Van",
                DateOfBirth = new DateTime(1985, 9, 29)
            },
            new(){
                IdPatient = 2,
                FirstName = "Vi",
                LastName = "Van",
                DateOfBirth = new DateTime(2000, 5, 1)
            }
        };
        
        var prescriptions = new List<Prescription>
        {
            new()
            {
                IdPrescription = 1,
                Date = new DateTime(2022, 1, 1),
                DueDate = new DateTime(2025, 5, 20),
                IdPatient = 1,
                IdDoctor = 2,
            },
            new()
            {
                IdPrescription = 2,
                Date = new DateTime(2024, 10, 10),
                DueDate = new DateTime(2026, 10, 20),
                IdPatient = 2,
                IdDoctor = 1,
            }
        };
        
        var meds = new List<Medicament>
        {
            new()
            {
                IdMedicament = 1,
                Name = "Ibuprom",
                Description = "descccccccccccccccc",
                Type = "tp"
            },
            new()
            {
                IdMedicament = 2,
                Name = "Apap",
                Description = "desc",
                Type = "TTTTTTTTTTTTTTTTTTTTTTPPPPPPPPPPPPPPPPPPPPPPPP"
            },
            new()
            {
                IdMedicament = 3,
                Name = "A",
                Description = "B",
                Type = "C"
            }
        };
        
        var pr_med = new List<PrescriptionMedicament>
        {
            new()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = null,
                Details = "det"
            },
            new()
            {
                IdMedicament = 3,
                IdPrescription = 1,
                Dose = 2,
                Details = "det2"
            },
            new()
            {
                IdMedicament = 2,
                IdPrescription = 2,
                Dose = null,
                Details = "deeeeetttt"
            }
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<Medicament>().HasData(meds);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(pr_med);
    }
}