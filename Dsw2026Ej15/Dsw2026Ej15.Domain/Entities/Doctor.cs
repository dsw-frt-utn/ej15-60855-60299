using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public  class Doctor : BaseEntity
    {
        public string Name {  get; init; }
        public string LicenseNumber {  get; init; }
        public bool IsActive {  get; private set; }
        public Speciality? speciality { get; private set; }
    }

}
