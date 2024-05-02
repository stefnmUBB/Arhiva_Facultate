using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    internal class GetImprumuturiByAbonatRequest : IRequest
    {
        public Abonat Abonat { get; set; }
        public GetImprumuturiByAbonatRequest(Abonat abonat)
        {
            Abonat = abonat;
        }
    }
}
