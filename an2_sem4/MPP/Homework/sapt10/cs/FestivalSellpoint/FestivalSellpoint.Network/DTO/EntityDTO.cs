using FestivalSellpoint.Network.Utils;
using System;

namespace FestivalSellpoint.Network.DTO
{
    [Serializable]
    public class EntityDTO : IStringifiable
    {
        public int Id { get; set; }
    }
}
