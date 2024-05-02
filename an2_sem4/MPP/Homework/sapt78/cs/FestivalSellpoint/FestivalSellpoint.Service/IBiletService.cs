using FestivalSellpoint.Domain;
using System.Collections.Generic;

namespace FestivalSellpoint.Service
{
    public interface IBiletService : IService<int, Bilet>
    {
        IEnumerable<Bilet> GetBySpectacol(Spectacol spectacol);
    }
}
