using FestivalSellpoint.Domain;
using FestivalSellpoint.Network.DTO;
using System;
using System.Linq;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class SpectacoleResponse : IResponse
    {
        public SpectacolDTO[] Spectacole { get; }

        public SpectacoleResponse(Spectacol[] spectacole)
            => Spectacole = spectacole.Select(SpectacolDTO.FromSpectacol).ToArray();
    }
}
