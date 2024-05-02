using FestivalSellpoint.Network.DTO;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class LoginAngajatResponse : IResponse
    {
        public AngajatDTO Angajat { get; }
        public LoginAngajatResponse(AngajatDTO angajat) => Angajat = angajat;
    }
}
