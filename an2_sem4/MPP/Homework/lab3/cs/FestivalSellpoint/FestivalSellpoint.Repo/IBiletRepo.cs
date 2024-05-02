using FestivalSellpoint.Domain;
using System.Collections.Generic;

namespace FestivalSellpoint.Repo
{
    public interface IBiletRepo : IRepo<int, Bilet>
    {
        IEnumerable<Bilet> getBySpectacol(Spectacol spectacol);
    }
}
