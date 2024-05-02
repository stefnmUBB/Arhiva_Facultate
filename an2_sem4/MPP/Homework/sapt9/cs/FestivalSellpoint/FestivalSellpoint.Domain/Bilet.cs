using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Domain
{
    public class Bilet : Entity<int>
    {
        public string NumeCumparator { get; set; }
        public int NrLocuri { get; set; }
        public Spectacol Spectacol { get; set; }

        public Bilet(string numeCumparator, int nrLocuri, Spectacol spectacol)
        {
            NumeCumparator = numeCumparator;
            NrLocuri = nrLocuri;
            Spectacol = spectacol;
        }

        public override string ToString()
            => $"Bilet {{ nume={NumeCumparator}, locuri={NrLocuri}, spectacol={Spectacol.Id} }}";
    }
}
