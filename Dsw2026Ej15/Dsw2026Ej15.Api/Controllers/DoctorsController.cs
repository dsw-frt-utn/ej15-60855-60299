using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctor")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("Nombre y matricula son requeridas.");
            }
            var speciality = await _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
            {
                throw new ValidationException("Especialidad no existe");
            }
            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            await _persistence.AddDoctor(doctor);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetActivedDoctors()
        {
            var doctors = await _persistence.GetAllDoctors();
            var activeDoctorsResponse = doctors.Where(d => d.IsActive).Select(d => new DoctorModel.Response(d.Id, d.Name,
                d.LicenseNumber, d.Speciality?.Name)).ToList();

            return Ok(activeDoctorsResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = (await GetDoctor(id))!;
            var response = new DoctorModel.Response(doctor.Id, doctor.Name,
                doctor.LicenseNumber, doctor.Speciality?.Name);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id)
        {
            var doctor = (await GetDoctor(id))!;
            doctor.Deactive();
            await _persistence.UpdateDoctor(doctor);
            return NoContent();
        }

        private async Task<Doctor?> GetDoctor(Guid id)
        {
            return await _persistence.GetDoctor(id) ?? throw new EntityNotFoundException("El medico solicitado no existe o no esta activo. ");
        }
    }
}