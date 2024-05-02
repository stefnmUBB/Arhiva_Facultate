using System.Xml.Serialization;

namespace LabCDiezFacultativ.Domain
{
    [XmlInclude(typeof(JucatorActiv))]
    public class Jucator : Elev
    {
        public Jucator()
        {
            Nume = "";
            Scoala = "";
            IdEchipa = -1;
        }
        public Jucator(string name, string scoala, long idEchipa) : base(name, scoala)
        {
            IdEchipa = idEchipa;
        }

        public long IdEchipa { get; set; }
    }
}
