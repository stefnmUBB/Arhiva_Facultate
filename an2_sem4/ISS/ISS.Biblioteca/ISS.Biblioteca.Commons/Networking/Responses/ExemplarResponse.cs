using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class ExemplarResponse : IResponse
    {
        public ExemplarCarte Exemplar { get; set; }

        public ExemplarResponse(ExemplarCarte exemplar)
        {
            Exemplar = exemplar;
        }
    }
}
