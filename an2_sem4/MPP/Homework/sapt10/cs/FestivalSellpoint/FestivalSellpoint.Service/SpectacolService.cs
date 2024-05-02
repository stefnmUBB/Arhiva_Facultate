using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo;
using System;
using System.Collections.Generic;

namespace FestivalSellpoint.Service
{
    public class SpectacolService : AbstractService<int, Spectacol, ISpectacolRepo>, ISpectacolService
    {
        public SpectacolService(ISpectacolRepo repo) : base(repo)
        {
        }

        public IEnumerable<Spectacol> GetBetweenDates(DateTime start, DateTime end)
            => Repo.GetBetweenDates(start, end);
    }
}
