using Hospital_CodeFirst.Exceptions;
using Hospital_CodeFirst.Models.DTOs;
using Hospital_CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_CodeFirst.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IDbService dbService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientAsync([FromRoute] int id)
    {
        try
        {
            return Ok(await dbService.GetPatientAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}