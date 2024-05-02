namespace LabCDiezFacultativ.Domain
{
    public class Echipa : Entity<long>
    {
        public string Nume { get; set; }

        public Echipa() { Nume = ""; }
        public Echipa(string nume)
        {
            Nume = nume;
        }
    }
}
