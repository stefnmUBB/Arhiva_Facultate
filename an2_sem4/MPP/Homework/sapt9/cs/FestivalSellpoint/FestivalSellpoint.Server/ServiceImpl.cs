using FestivalSellpoint.Domain;
using FestivalSellpoint.Service;
using FestivalSellpoint.Service.Observer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FestivalSellpoint.Server
{
    internal class ServiceImpl : IAppService
    {
        private readonly IAppService InnerService;
        private readonly Dictionary<string, IObserver> LoggedClients = new Dictionary<string, IObserver>();

        public ServiceImpl(IAppService innerService)
        {
            InnerService = innerService;
        }        
        
        public IEnumerable<Spectacol> FilterSpectacole(DateTime day) => InnerService.FilterSpectacole(day);        

        public IEnumerable<Spectacol> GetAllSpectacole() => InnerService.GetAllSpectacole();           

        public Angajat LoginAngajat(string username, string password, IObserver client)
        {            
            var angajat = InnerService.LoginAngajat(username, password);
            if (angajat == null)
                throw new ServiceException("Authentification failed");

            if (LoggedClients.ContainsKey(angajat.Username))
                throw new ServiceException("Angajat already logged");

            LoggedClients[angajat.Username] = client;
            Console.WriteLine($"Logged in : {angajat.Username}");

            return angajat;            
        }
        public void ReserveBilet(Spectacol spectacol, string cumparatorName, int seats)
        {
            InnerService.ReserveBilet(spectacol, cumparatorName, seats);
            Console.WriteLine($"Updated spectacol : {spectacol}");
            Task.Run(() =>
            {
                foreach (var obs in LoggedClients.Values)
                    obs.UpdateSpectacol(spectacol);
            });
        }

        public void RegisterAngajat(string username, string password, string email) => throw new NotImplementedException();
        public IEnumerable<Spectacol> FilterSpectacole(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
    }
}
