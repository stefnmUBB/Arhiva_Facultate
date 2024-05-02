using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Domain
{
    public class Spectacol : Entity<int>
    {
        public string Artist { get; set; }
        public DateTime Data { get; set; }
        public string Locatie { get; set; }
        public int NrLocuriDisponibile { get; set; }
        public int NrLocuriVandute { get; set; }

        public Spectacol(string artist, DateTime data, string locatie, int nrLocuriDisponibile, int nrLocuriVandute)
        {
            Artist = artist;
            Data = data;
            Locatie = locatie;
            NrLocuriDisponibile = nrLocuriDisponibile;
            NrLocuriVandute = nrLocuriVandute;
        }

        public override string ToString()
            => $"Spectacol {{artist={Artist}, data={Data}, loc={Locatie}, disp={NrLocuriDisponibile}, vand={NrLocuriVandute}}}";
    }
}
