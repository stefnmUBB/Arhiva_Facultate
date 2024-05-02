using FestivalSellpoint.Domain;
using FestivalSellpoint.Service.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Service
{
    public class AppService : IAppService
    {
        private IAngajatService AngajatService;
        private IBiletService BiletService;
        private ISpectacolService SpectacolService;

        public AppService(IAngajatService angajatService, IBiletService biletService, ISpectacolService spectacolService)
        {
            AngajatService = angajatService;
            BiletService = biletService;
            SpectacolService = spectacolService;
        }

        public Angajat LoginAngajat(string username, string password, IObserver client = null)
        {
            return AngajatService.Login(username, password);
        }        

        public void RegisterAngajat(string username, string password, string email)
        {
            AngajatService.Register(new Angajat(username, password, email));
        }

        public IEnumerable<Spectacol> GetAllSpectacole()
            => SpectacolService.GetAll();

        public IEnumerable<Spectacol> FilterSpectacole(DateTime startDate, DateTime endDate)
            => SpectacolService.GetBetweenDates(startDate, endDate);

        public IEnumerable<Spectacol> FilterSpectacole(DateTime day)
        {
            var y = day.Year;
            var m = day.Month;
            var d = day.Day;
            return FilterSpectacole(new DateTime(y, m, d, 0, 0, 0), new DateTime(y, m, d, 23, 59, 59));
        }

        public void ReserveBilet(Spectacol spectacol, string cumparatorName, int seats)
        {
            var bilet = new Bilet(cumparatorName, seats, spectacol);

            if(seats > spectacol.NrLocuriDisponibile)
            {
                throw new ArgumentException("More seats requested than there are provided");
            }
            BiletService.Add(bilet);

            spectacol.NrLocuriDisponibile -= seats;
            spectacol.NrLocuriVandute += seats;
            SpectacolService.Update(spectacol);
        }
    }
}
