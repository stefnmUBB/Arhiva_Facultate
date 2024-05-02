using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class UpdateExemplarImprumutatResponse : IUpdateResponse
    {
        public Imprumut Imprumut { get; set; }

        public UpdateExemplarImprumutatResponse(Imprumut imprumut)
        {
            Imprumut = imprumut;
        }
    }
}
