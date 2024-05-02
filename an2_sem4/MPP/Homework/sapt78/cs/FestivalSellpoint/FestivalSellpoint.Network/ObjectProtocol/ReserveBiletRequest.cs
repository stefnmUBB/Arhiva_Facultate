using FestivalSellpoint.Network.DTO;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class ReserveBiletRequest : IRequest
    {
        public BiletDTO Bilet { get; }
        public ReserveBiletRequest(BiletDTO bilet) => Bilet = bilet;
    }
}
