using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public class Speciality : BaseEntity
    {
        string Name { get; set; }
        public string Description { get; set; }

       /* public Speciality(string name)
        {
            this.Name = name;
        }
        */
    }
}
