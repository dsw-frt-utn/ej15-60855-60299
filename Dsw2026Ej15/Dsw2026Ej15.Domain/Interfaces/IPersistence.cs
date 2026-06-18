using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        void AddDoctor(Doctor doctor);
        Speciality? GetSpecialityById(Guid id);
        List<Doctor> GetAllDoctors();

        Doctor? GetDoctor(Guid id);
        void  DeleteDoctor(Guid id);


        

    }
}
