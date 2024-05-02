using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    internal class FilteredSpectacoleResponse : SpectacoleResponse
    {
        public FilteredSpectacoleResponse(Spectacol[] spectacole) : base(spectacole){ }
    }
}
