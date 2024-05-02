using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class AbonatResponse : IResponse
    {
        public Abonat Abonat { get; set; }

        public AbonatResponse(Abonat abonat)
        {
            Abonat = abonat;
        }
    }
}
