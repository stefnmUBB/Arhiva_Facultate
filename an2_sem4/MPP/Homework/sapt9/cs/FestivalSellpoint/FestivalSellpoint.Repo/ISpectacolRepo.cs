using FestivalSellpoint.Domain;
using System;
using System.Collections.Generic;

namespace FestivalSellpoint.Repo
{
    public interface ISpectacolRepo : IRepo<int, Spectacol>
    {
        IEnumerable<Spectacol> GetBetweenDates(DateTime start, DateTime end);        
    }
}
