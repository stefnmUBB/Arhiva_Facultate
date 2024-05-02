using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    internal class ImprumutResponse : IResponse
    {
        public Imprumut Imprumut { get; set; }

        public ImprumutResponse(Imprumut imprumut)
        {
            Imprumut = imprumut;
        }
    }
}
