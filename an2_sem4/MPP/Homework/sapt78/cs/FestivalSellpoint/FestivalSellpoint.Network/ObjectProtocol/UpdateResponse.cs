using FestivalSellpoint.Network.DTO;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class UpdateResponse : IResponse
    {
        public SpectacolDTO Spectacol { get; }
        public UpdateResponse(SpectacolDTO spectacol) => Spectacol = spectacol;
    }
}
