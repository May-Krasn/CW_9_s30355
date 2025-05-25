using Hospital_CodeFirst.DTOs;
using Hospital_CodeFirst.Exceptions;
using Hospital_CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_CodeFirst.Controllers;



[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService dbService) : ControllerBase
{
    
    // http://localhost:0000/Prescription
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionSetDto prescriptionData)
    {
        try
        {
            var prescription = await dbService.CreatePrescriptionAsync(prescriptionData);
            return Created("", prescription);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}