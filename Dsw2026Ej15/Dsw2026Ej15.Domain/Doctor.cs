using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public  class Doctor : BaseEntity
    {
        string Name {  get; set; }
        string LicenseNumber {  get; set; }
        bool IsActive {  get; set; }
        Speciality speciality { get; set; }
    }

}
