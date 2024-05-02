namespace LabCDiezFacultativ.Domain
{    
    public class JucatorActiv : Entity<long>
    {
        public long IdJucator { get; set; }
        public long IdMeci { get; set; }
        public int NrPuncteInscrise { get; set; }

        public TipJucatorActiv Tip { get; set; }

        public JucatorActiv()
        {
            IdJucator = -1;
            IdMeci = -1;
            NrPuncteInscrise = 0;
            Tip = TipJucatorActiv.Rezerva;
        }

        public JucatorActiv(long idJucator, long idMeci, int nrPuncteInscrise, TipJucatorActiv tip)
        {
            IdJucator = idJucator;
            IdMeci = idMeci;
            NrPuncteInscrise = nrPuncteInscrise;
            Tip = tip;
        }
    }

    public enum TipJucatorActiv { Rezerva, Participant }
}
