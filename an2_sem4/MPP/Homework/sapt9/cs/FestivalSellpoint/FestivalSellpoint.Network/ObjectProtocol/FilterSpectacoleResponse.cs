using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    internal class FilterSpectacoleResponse : SpectacoleResponse
    {
        public FilterSpectacoleResponse(Spectacol[] spectacole) : base(spectacole){ }
    }
}
