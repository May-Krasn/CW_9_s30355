using Hospital_CodeFirst.Models.DTOs;

namespace Hospital_CodeFirst.DTOs;


public class MedicamentGetDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}
public class PresMedsGetDto
{
    public PatientGetDto Patient { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}