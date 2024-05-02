using ISS.Biblioteca.Commons.Domain;
using System.Collections.Generic;

namespace ISS.Biblioteca.Commons.Service
{
    public interface IService
    {
        IEnumerable<(Carte Carte, int NrExemplare)> GetCarti();
        IEnumerable<(Carte Carte, int NrExemplare)> GetCartiDisponibile();
        Utilizator Login(string cnp, string token, IClientObserver clientObserver = null);
        void Logout(Utilizator utilizator);
        ExemplarCarte GetDisponibleExemplarOf(Carte carte);
        IEnumerable<Imprumut> GetImprumuturiByAbonat(Abonat abonat);
        Imprumut EfectueazaImprumut(Abonat abonat, ExemplarCarte Carti);
        Retur EfectueazaRetur(Bibliotecar bibliotecar, Imprumut imprumut);
        Utilizator Register(Utilizator utilizator);
        Abonat GetAbonatByCnp(string cnp);
    }
}
