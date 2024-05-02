using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo;
using System.Collections.Generic;

namespace FestivalSellpoint.Service
{
    public class BiletService : AbstractService<int, Bilet, IBiletRepo>, IBiletService
    {
        public BiletService(IBiletRepo repo) : base(repo)
        {
        }

        public IEnumerable<Bilet> GetBySpectacol(Spectacol spectacol)
            => Repo.GetBySpectacol(spectacol);
    }
}
