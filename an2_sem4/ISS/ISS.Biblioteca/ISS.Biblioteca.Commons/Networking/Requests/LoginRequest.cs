using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    public class LoginRequest : IRequest
    {
        public string Cnp { get; set; }
        public string Token { get; set; }

        public LoginRequest(string cnp, string token)
        {
            Cnp = cnp;
            Token = token;
        }
    }
}
