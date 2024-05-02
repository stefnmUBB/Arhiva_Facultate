using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    internal class GetAllSpectacoleResponse : SpectacoleResponse
    {
        public GetAllSpectacoleResponse(Spectacol[] spectacole) : base(spectacole) { }  
    }
}
