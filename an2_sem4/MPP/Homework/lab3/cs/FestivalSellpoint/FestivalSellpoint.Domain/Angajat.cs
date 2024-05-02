using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Domain
{
    public class Angajat : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Angajat(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public override string ToString()        
            => $"Angajat {{ user={Username}, pass={Password}, email={Email} }}";        
    }
}
