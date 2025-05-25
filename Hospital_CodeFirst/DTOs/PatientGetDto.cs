using Hospital_CodeFirst.DTOs;

namespace Hospital_CodeFirst.Models.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<PrescriptionGetDto>? Prescriptions { get; set; }
}

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; }
    public DoctorGetDto Doctor { get; set; }
    public Patient Patient { get; set; }
}
