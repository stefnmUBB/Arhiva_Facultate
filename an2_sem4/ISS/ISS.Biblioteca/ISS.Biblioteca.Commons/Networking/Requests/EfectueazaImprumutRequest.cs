using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    public class EfectueazaImprumutRequest : IRequest
    {
        public Abonat Abonat { get; set; }
        public ExemplarCarte Exemplar { get; set; }

        public EfectueazaImprumutRequest(Abonat abonat, ExemplarCarte exemplar)
        {
            Abonat = abonat;
            Exemplar = exemplar;
        }
    }
}
