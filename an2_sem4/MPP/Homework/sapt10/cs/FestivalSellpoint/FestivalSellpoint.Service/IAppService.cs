using FestivalSellpoint.Domain;
using System.Collections.Generic;
using System;
using FestivalSellpoint.Service.Observer;

namespace FestivalSellpoint.Service
{
    public interface IAppService
    {
        Angajat LoginAngajat(string username, string password, IObserver client = null);
        void RegisterAngajat(string username, string password, string email);

        IEnumerable<Spectacol> GetAllSpectacole();

        IEnumerable<Spectacol> FilterSpectacole(DateTime startDate, DateTime endDate);

        IEnumerable<Spectacol> FilterSpectacole(DateTime day);

        void ReserveBilet(Spectacol spectacol, string cumparatorName, int seats);
    }
}
