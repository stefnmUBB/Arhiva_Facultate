using FestivalSellpoint.Domain;
using System.Collections.Generic;
using System;

namespace FestivalSellpoint.Service
{
    public interface ISpectacolService : IService<int, Spectacol>
    {
        IEnumerable<Spectacol> GetBetweenDates(DateTime start, DateTime end);
    }
}
