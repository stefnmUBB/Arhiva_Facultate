using FestivalSellpoint.Domain;
using System;
using System.Collections.Generic;

namespace FestivalSellpoint.Repo
{
    public interface ISpectacolRepo : IRepo<int, Spectacol>
    {
        IEnumerable<Spectacol> getBetweenDates(DateTime start, DateTime end);
    }
}
