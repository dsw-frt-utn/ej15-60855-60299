using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
            LoadSpecialities();
        }

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _context.Doctors.Include(doctor => doctor.Speciality).ToList();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _context.Specialities.FirstOrDefault(speciality => speciality.Id == id);
        }

        public Doctor? GetDoctor(Guid id)
        {
            return _context.Doctors.Include(doctor => doctor.Speciality).FirstOrDefault(doctor => doctor.Id == id);
        }

        public void DeleteDoctor(Guid id)
        {
            Doctor? doctor = GetDoctor(id);

            if (doctor != null)
            {
                doctor.Deactivate();
                _context.SaveChanges();
                
            }
        }

        private void LoadSpecialities()
        {
            try
            {
                if (!_context.Specialities.Any())
                {
                    string jsonpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                    var json = File.ReadAllText(jsonpath);
                    var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                        new JsonSerializerOptions()
                        {
                            PropertyNameCaseInsensitive = true,
                        }) ?? [];
                    _context.Specialities.AddRange(specialities.Select(s => new Speciality(s.Name, s.Description, s.Id)));
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}