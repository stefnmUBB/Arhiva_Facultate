using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISS.Biblioteca.Commons.Service
{
    public class BaseService : IService
    {
        private readonly IAbonatRepo AbonatRepo;
        private readonly IAdministratorITRepo AdministratorITRepo;
        private readonly IBibliotecarRepo BibliotecarRepo;
        private readonly ICarteRepo CarteRepo;
        private readonly IExemplarCarteRepo ExemplarCarteRepo;
        private readonly IImprumutRepo ImprumutRepo;
        private readonly IReturRepo ReturRepo;

        public BaseService(IAbonatRepo abonatRepo, IAdministratorITRepo administratorITRepo, IBibliotecarRepo bibliotecarRepo, ICarteRepo carteRepo, IExemplarCarteRepo exemplarCarteRepo, IImprumutRepo imprumutRepo, IReturRepo returRepo)
        {
            AbonatRepo = abonatRepo;
            AdministratorITRepo = administratorITRepo;
            BibliotecarRepo = bibliotecarRepo;
            CarteRepo = carteRepo;
            ExemplarCarteRepo = exemplarCarteRepo;
            ImprumutRepo = imprumutRepo;
            ReturRepo = returRepo;
        }

        public Imprumut EfectueazaImprumut(Abonat abonat, ExemplarCarte carte)
        {
            var imprumut = new Imprumut
            {
                Data = DateTime.Now,
                Status = StatusImprumut.Activ,
                Abonat = abonat,
                CarteImprumutata = carte
            };
            imprumut = ImprumutRepo.Add(imprumut);

            carte.Status = StatusCarte.Imprumutat;
            ExemplarCarteRepo.Update(carte);
            return imprumut;
        }

        public Retur EfectueazaRetur(Bibliotecar bibliotecar, Imprumut imprumut)
        {            
            var retur = new Retur
            {
                Data = DateTime.Now,
                Abonat = imprumut.Abonat,
                CarteReturnata = imprumut.CarteImprumutata,
                Bibliotecar = bibliotecar
            };
            var result = ReturRepo.Add(retur);
            if (result != null) 
            {
                imprumut.Status = StatusImprumut.Returnat;                
                ImprumutRepo.Update(imprumut);
                imprumut.CarteImprumutata.Status = StatusCarte.Disponibil;
                ExemplarCarteRepo.Update(imprumut.CarteImprumutata);
            }
            return result;
        }

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCartiDisponibile()
            => CarteRepo.GetCartiDisponibile();

        public ExemplarCarte GetDisponibleExemplarOf(Carte carte)
            => ExemplarCarteRepo.GetDisponibleExemplarOf(carte);        

        public Utilizator Login(string cnp, string token, IClientObserver clientObserver = null)
        {
            return (Utilizator)AdministratorITRepo.FindByCredentials(cnp, token)
                ?? (Utilizator)AbonatRepo.FindByCredentials(cnp, token)
                ?? BibliotecarRepo.FindByCredentials(cnp, token);
        }

        Random Random = new Random();

        private string GenerateNewToken()
        {
            return (Random.Next() % 10000).ToString().PadLeft(4, '0');
        }

        public Utilizator Register(Utilizator utilizator)
        {
            utilizator.TokenLogare = GenerateNewToken();
            if (utilizator is AdministratorIT admin)
                return AdministratorITRepo.Add(admin);
            if (utilizator is Abonat abonat)
                return AbonatRepo.Add(abonat);
            if (utilizator is Bibliotecar bibliotecar)
                return BibliotecarRepo.Add(bibliotecar);
            return null;
        }

        public void Logout(Utilizator utilizator) { }

        public IEnumerable<Imprumut> GetImprumuturiByAbonat(Abonat abonat)
            => ImprumutRepo.GetByAbonat(abonat);

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCarti()
            => CarteRepo.GetCarti();

        public Abonat GetAbonatByCnp(string cnp) => AbonatRepo.GetByCnp(cnp);
    }
}
