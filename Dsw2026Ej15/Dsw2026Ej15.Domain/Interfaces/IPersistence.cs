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


        //el profesor usa solo los 3 de arriba los otros dos de abajo no o los usa
        Doctor? GetDoctor(Guid id);
        void  DeleteDoctor(Guid id);


        

    }
}
