using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    public class GetDisponibleExemplarOfRequest : IRequest
    {
        public Carte Carte { get; set; }

        public GetDisponibleExemplarOfRequest(Carte carte)
        {
            Carte = carte;
        }
    }
}
