using System.Data;
using Hospital_CodeFirst.Data;
using Hospital_CodeFirst.DTOs;
using Hospital_CodeFirst.Exceptions;
using Hospital_CodeFirst.Models;
using Hospital_CodeFirst.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital_CodeFirst.Services;

public interface IDbService
{
    public Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionSetDto prescription);
    public Task<PatientGetDto> GetPatientAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{

    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionSetDto prescriptionDto)
    {
        var patient = data.Patients.FirstOrDefault(p => p.IdPatient == prescriptionDto.Patient.IdPatient);
        if (patient == null)
        {
            var newPatient = new Patient()
            {
                FirstName = prescriptionDto.Patient.FirstName,
                LastName = prescriptionDto.Patient.LastName,
                DateOfBirth = prescriptionDto.Patient.DateOfBirth,
            };
            await data.Patients.AddAsync(newPatient);
            await data.SaveChangesAsync();

            patient = newPatient;
        }
        
        Doctor? doc = data.Doctors.FirstOrDefault(d => d.IdDoctor == prescriptionDto.Doctor.IdDoctor);
        if (doc == null)
        {
            throw new NotFoundException($"Doctor {prescriptionDto.Doctor.IdDoctor} not found");
        }

        if (prescriptionDto.Medicaments.Count > 10)
            throw new TooManyException("Medicament count is too big for prescription");

        foreach (var meds in prescriptionDto.Medicaments)
        {
            if (!await data.Medicaments.AnyAsync(m => m.IdMedicament == meds.IdMedicament))
                throw new NotFoundException($"Medicament {meds.IdMedicament} not found");
        }

        if (prescriptionDto.DueDate < prescriptionDto.Date)
            throw new DataException("Due date cannot be earlier than date");

        var pres = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            Patient = patient,
            Doctor = doc,
            IdDoctor = doc.IdDoctor,
            IdPatient = patient.IdPatient
        };
        
        await data.Prescriptions.AddAsync(pres);
        await data.SaveChangesAsync();

        return new PrescriptionGetDto()
        {
            IdPrescription = pres.IdPrescription,
            Medicaments = await data.Medicaments.Select(m => new MedicamentGetDto()
            {
                IdMedicament = m.IdMedicament,
                Description = m.Description,
                Name = m.Name,
                Type = m.Type
            }).ToListAsync(),
            Date = pres.Date,
            DueDate = pres.DueDate
        };
    }
    

    public async Task<PatientGetDto> GetPatientAsync(int id)
    {
        var result = await data.Patients.Select(pt => new PatientGetDto
        {
            IdPatient = pt.IdPatient,
            FirstName = pt.FirstName,
            LastName = pt.LastName,
            DateOfBirth = pt.DateOfBirth,
            Prescriptions = pt.Prescriptions.OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionGetDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Medicaments = pr.PrescriptionMedicaments.Select(meds => new MedicamentGetDto
                {
                    IdMedicament = meds.IdMedicament,
                    Name = meds.Medicament.Name,
                    Description = meds.Medicament.Description,
                    Type = meds.Medicament.Type
                }).ToList(),
                Doctor = new DoctorGetDto
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName,
                }
            }).ToList()
        }).FirstOrDefaultAsync(e => e.IdPatient == id);

        return result ?? throw new NotFoundException($"Patient with id {id} not found");
    }

}