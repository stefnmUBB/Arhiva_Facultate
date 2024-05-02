using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class LoginResponse : IResponse
    {
        public Utilizator Utilizator { get; set; }

        public LoginResponse(Utilizator utilizator)
        {
            Utilizator = utilizator;
        }
    }
}
