using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Network.DTO
{
    [Serializable]
    public class AngajatDTO : EntityDTO
    {
        public string Username { get; }
        public string Password { get; }

        public AngajatDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public static AngajatDTO FromAngajat(Angajat a)
            => new AngajatDTO(a.Username, a.Password) { Id = a.Id };

        public Angajat ToAngajat()
            => new Angajat(Username, Password, "dummy@notusedemail.com") { Id = Id };
    }
}
