
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

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
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("El nombre es requerido");
            }

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("La matrícula es requerida");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);

            if (speciality == null)
            {
                throw new ValidationException("La especialidad debe existir");
            }

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);

            _persistence.AddDoctor(doctor);

            return Created();

        }

        [HttpGet("doctors")]

        public async Task<IActionResult> AllDoctors()
        {

            List<Doctor> doctors = _persistence.GetAllDoctors().Where(doctor => doctor.IsActive).ToList();
            return Ok(doctors);

        }

        [HttpGet("doctors/{id}")]


        public async Task<IActionResult> ActiveDoctor(Guid id)
        {
            Doctor? doctor = _persistence.GetDoctor(id);

            if (doctor is null || doctor.IsActive == false)
            {
                return NotFound("No se encontro el doctor");
            }

            var response = new { doctor.Name, doctor.LicenseNumber, SpecialityName = doctor.Speciality.Name };

            return Ok(response);

        }

        [HttpDelete("doctors/{id}")]

        public async Task<IActionResult> RemoveDoctor(Guid id)
        {
            Doctor? doctor = _persistence.GetDoctor(id);

            if (doctor is null || doctor.IsActive == false)
            {
                return NotFound("No se encontro el doctor");
            }

           _persistence.DeleteDoctor(id);

            return NoContent();
        }



    }
}

