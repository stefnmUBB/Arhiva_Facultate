using System.Xml.Serialization;

namespace LabCDiezFacultativ.Domain
{
    [XmlInclude(typeof(Echipa))]
    [XmlInclude(typeof(Elev))]
    [XmlInclude(typeof(Jucator))]
    [XmlInclude(typeof(JucatorActiv))]
    [XmlInclude(typeof(Meci))]
    public abstract class Entity<ID>
    {
        public ID Id { get; set; }       
    }
}
