using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;


namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {

        private  List<Doctor> _doctors = [];
        private  List<Speciality> _specialities = [];

        
        public PersistenceInMemory() 
        {
             LoadSpecialities();
        }

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctors.ToList();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(speciality => speciality.Id == id);
        }
       

        public Doctor? GetDoctor(Guid id)
        {
            return _doctors.FirstOrDefault(doctor => doctor.Id == id);
        
        }
        public void DeleteDoctor(Guid id)
        {
            Doctor? doctor = GetDoctor(id);

            if (doctor != null)
            {
                doctor.Deactivate();
            }
        }

        public void LoadSpecialities()
        {
            try
            {
                string jsonpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                var json = File.ReadAllText(jsonpath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    }) ?? [];
                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch (Exception)
            {

            }
        }


    }
}
