using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class LoginAngajatRequest : IRequest
    {
        public string Username { get; }
        public string Password { get; }

        public LoginAngajatRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
