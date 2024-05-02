using FestivalSellpoint.Network.DTO;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class UpdatedSpectacolResponse : IResponse
    {
        public SpectacolDTO Spectacol { get; }
        public UpdatedSpectacolResponse(SpectacolDTO spectacol) => Spectacol = spectacol;
    }
}
