using Newtonsoft.Json;

namespace FestivalSellpoint.RestDemo
{
    public class Spectacol
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("data")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Data { get; set; }

        [JsonProperty("locatie")]
        public string Locatie { get; set; }
        
        [JsonProperty("nrLocuriDisponibile")]        
        public int NrLocuriDisponibile { get; set; }

        [JsonProperty("nrLocuriVandute")]
        public int NrLocuriVandute { get; set; }                    

        public override string ToString()
        {
            return $"Spectacol{{Id={Id},Artist={Artist},Data={Data},Loc={Locatie},NrLDisp={NrLocuriDisponibile}, NrLVand={NrLocuriVandute}}}";            
        }

        public Spectacol(string artist, DateTime data, string locatie, int nrLocuriDisponibile, int nrLocuriVandute)
        {            
            Artist = artist;
            Data = data;
            Locatie = locatie;
            NrLocuriDisponibile = nrLocuriDisponibile;
            NrLocuriVandute = nrLocuriVandute;
        }
    }
}
