using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Server
{
    internal class ServiceImpl : IService
    {
        private readonly IService InnerService;
        private readonly Dictionary<string, IClientObserver> LoggedClients = new Dictionary<string, IClientObserver>();

        public ServiceImpl(IService innerService)
        {
            InnerService = innerService;
        }

        public Imprumut EfectueazaImprumut(Abonat abonat, ExemplarCarte exemplar)
        {
            var imprumut = InnerService.EfectueazaImprumut(abonat, exemplar);
            foreach(var client in LoggedClients.Values)
            {
                client?.OnImprumut(imprumut);
            }
            return imprumut;
        }

        public Retur EfectueazaRetur(Bibliotecar bibliotecar, Imprumut imprumut)
        {
            var retur = InnerService.EfectueazaRetur(bibliotecar, imprumut);
            foreach (var client in LoggedClients.Values)
            {
                client?.OnRetur(retur);
            }
            return retur;
        }

        public Abonat GetAbonatByCnp(string cnp)
        {
            var abonat = InnerService.GetAbonatByCnp(cnp);
            if (abonat == null)
                throw new ArgumentException("N-a fost gasit niciun abonat");
            return abonat;
        }

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCarti() => InnerService.GetCarti();        

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCartiDisponibile() => InnerService.GetCartiDisponibile();

        public ExemplarCarte GetDisponibleExemplarOf(Carte carte)
        {
            var exemplar = InnerService.GetDisponibleExemplarOf(carte);
            if (exemplar == null)
                throw new ArgumentException($"Niciun exemplar disponibil pentru '{carte.Titlu}' {carte.Autor}");
            return exemplar;
        }

        public IEnumerable<Imprumut> GetImprumuturiByAbonat(Abonat abonat) => InnerService.GetImprumuturiByAbonat(abonat);

        public Utilizator Login(string cnp, string token, IClientObserver clientObserver = null)
        {
            var utilizator = InnerService.Login(cnp, token, null);

            if (utilizator == null)
                throw new ArgumentException("Login failed");

            LoggedClients[utilizator.Cnp] = clientObserver;

            return utilizator;
        }

        public void Logout(Utilizator utilizator)
        {
            throw new NotImplementedException();
        }

        public Utilizator Register(Utilizator utilizator) => InnerService.Register(utilizator);        
    }
}
