using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    public class RegisterRequest : IRequest
    {
        public Utilizator Utilizator { get; set; }

        public RegisterRequest(Utilizator utilizator)
        {
            Utilizator = utilizator;
        }
    }
}
