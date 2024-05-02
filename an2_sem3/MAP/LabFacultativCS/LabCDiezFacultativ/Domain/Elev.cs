using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Domain
{
    public class Elev : Entity<long>
    {
        public Elev() { Nume = ""; Scoala = ""; }
        public Elev(string nume, string scoala)
        {
            Nume = nume;
            Scoala = scoala;
        }

        public string Nume { get; set; }
        public string Scoala { get; set; }
    }
}
