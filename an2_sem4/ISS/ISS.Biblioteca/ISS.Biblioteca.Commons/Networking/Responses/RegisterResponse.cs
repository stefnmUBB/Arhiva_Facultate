using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class RegisterResponse: IResponse
    {
        public Utilizator Utilizator { get; set; }

        public RegisterResponse(Utilizator utilizator)
        {
            Utilizator = utilizator;
        }
    }
}
