using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FestivalSellpoint.Repo.Models
{
    public partial class Spectacol
    {
        public Spectacol()
        {
            Bilet = new HashSet<Bilet>();
        }

        public long Id { get; set; }
        public string Artist { get; set; }
        public byte[] Data { get; set; }
        public string Locatie { get; set; }
        public long NrLocuriDisponibile { get; set; }
        public long NrLocuriVandute { get; set; }

        public virtual ICollection<Bilet> Bilet { get; set; }
    }
}
