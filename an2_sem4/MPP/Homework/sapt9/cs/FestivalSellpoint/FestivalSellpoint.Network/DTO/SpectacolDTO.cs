using FestivalSellpoint.Domain;
using FestivalSellpoint.Network.Utils;
using System;

namespace FestivalSellpoint.Network.DTO
{
    [Serializable]
    public class SpectacolDTO : EntityDTO
    {
        public string Artist { get; }

        private string _Data;

        public DateTime Data
        {
            get => DateTime.ParseExact(_Data, Constants.DTODateTimeFormatter, null);
            private set => _Data = value.ToString(Constants.DTODateTimeFormatter);
        }

        public string Locatie { get; }
        public int NrLocuriDisponibile { get; }
        public int NrLocuriVandute { get; }

        public SpectacolDTO(string artist, DateTime data, string locatie, int nrLocuriDisponibile, int nrLocuriVandute)
        {
            Artist = artist;
            Data = data;
            Locatie = locatie;
            NrLocuriDisponibile = nrLocuriDisponibile;
            NrLocuriVandute = nrLocuriVandute;
            Console.WriteLine(_Data);
        }

        public static SpectacolDTO FromSpectacol(Spectacol s)
            => new SpectacolDTO(s.Artist, s.Data, s.Locatie, s.NrLocuriDisponibile, s.NrLocuriVandute) { Id = s.Id };
        public Spectacol ToSpectacol()
            => new Spectacol(Artist, Data, Locatie, NrLocuriDisponibile, NrLocuriVandute) { Id = Id };
    }
}
