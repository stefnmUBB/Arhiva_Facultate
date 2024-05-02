using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Networking.Requests;
using ISS.Biblioteca.Commons.Networking.Responses;
using ISS.Biblioteca.Commons.Service;
using System;
using System.Collections.Generic;

namespace ISS.Biblioteca.Commons.Networking.Client
{
    public class ServiceObjectProxy : ServiceObjectProxyBase, IService
    {
        public ServiceObjectProxy(string host, int port) : base(host, port) { }

        public Imprumut EfectueazaImprumut(Abonat abonat, ExemplarCarte exemplar)
        {
            SendRequest(new EfectueazaImprumutRequest(abonat, exemplar));
            var resp = AwaitResponse<ImprumutResponse>();
            return resp.Imprumut;
        }

        public Retur EfectueazaRetur(Bibliotecar bibliotecar, Imprumut imprumut)
        {
            SendRequest(new ReturRequest(bibliotecar, imprumut));
            var resp = AwaitResponse<ReturResponse>();
            return resp.Retur;
        }

        public Abonat GetAbonatByCnp(string cnp)
        {
            SendRequest(new GetAbonatByCnpRequest(cnp));
            var resp = AwaitResponse<AbonatResponse>();
            return resp.Abonat;

        }

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCarti()
        {
            SendRequest(new GetCartiRequest());
            var resp = AwaitResponse<GetCartiListReponse>();
            return resp.Carti;
        }

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCartiDisponibile()
        {
            SendRequest(new GetCartiDisponibileRequest());
            var resp = AwaitResponse<GetCartiListReponse>();
            return resp.Carti;
        }

        public ExemplarCarte GetDisponibleExemplarOf(Carte carte)
        {
            SendRequest(new GetDisponibleExemplarOfRequest(carte));
            var resp = AwaitResponse<ExemplarResponse>();
            return resp.Exemplar;
        }

        public IEnumerable<Imprumut> GetImprumuturiByAbonat(Abonat abonat)
        {
            SendRequest(new GetImprumuturiByAbonatRequest(abonat));
            var resp = AwaitResponse<ImprumuturiByAbonatResponse>();
            return resp.Imprumuturi;
        }

        public Utilizator Login(string cnp, string token, IClientObserver clientObserver = null)
        {
            InitializeConnection();
            SendRequest(new LoginRequest(cnp, token));
            try
            {
                var resp = AwaitResponse<LoginResponse>();
                Client = clientObserver;
                return resp.Utilizator;
            }
            catch(Exception e)
            {
                CloseConnection();
                throw e;
            }            
        }

        public void Logout(Utilizator utilizator)
        {
            CloseConnection();
        }

        public Utilizator Register(Utilizator utilizator)
        {
            SendRequest(new RegisterRequest(utilizator));
            var resp = AwaitResponse<RegisterResponse>();
            return resp.Utilizator;            
        }

        protected override void HandleUpdate(IUpdateResponse update)
        {            
            try
            {
                if (update is UpdateExemplarImprumutatResponse imprumutatResponse)
                    Client.OnImprumut(imprumutatResponse.Imprumut);
                else if (update is UpdateExemplarReturnatResponse returnatResponse)
                    Client.OnRetur(returnatResponse.Retur);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new ProxyException(e);
            }
        }        
    }
}
