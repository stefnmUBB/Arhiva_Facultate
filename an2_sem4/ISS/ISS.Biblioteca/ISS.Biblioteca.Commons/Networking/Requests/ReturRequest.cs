using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    public class ReturRequest : IRequest
    {        
        public Bibliotecar Bibliotecar { get; set; }
        public Imprumut Imprumut { get; set; }

        public ReturRequest(Bibliotecar bibliotecar, Imprumut imprumut)
        {
            Bibliotecar = bibliotecar;
            Imprumut = imprumut;
        }
    }
}
