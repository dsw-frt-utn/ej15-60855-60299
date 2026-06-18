
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Api.Controllers
{

    [ApiController]
    [Route("api")]

    public class DoctorsController : ControllerBase 
    {
        public readonly IPersistence _persistence;
        public DoctorsController(IPersistence persistence) 
        {
            _persistence = persistence;
        }



        [HttpPost("doctors")]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request) 
        {
            if( string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y licencia requerido");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if(speciality == null) 
            {
                return BadRequest("Especialidad debe existir ");
            }
            return Created();

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            _persistence.AddDoctor(doctor);

        }





    }
}
