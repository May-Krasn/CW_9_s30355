using Hospital_CodeFirst.Models.DTOs;

namespace Hospital_CodeFirst.DTOs;

public class PrescriptionSetDto
{
    public PatientGetDto Patient { get; set; }
    public DoctorGetDto Doctor { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}