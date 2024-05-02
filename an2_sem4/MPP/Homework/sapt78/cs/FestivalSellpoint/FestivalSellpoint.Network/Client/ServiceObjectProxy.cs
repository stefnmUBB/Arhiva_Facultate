using FestivalSellpoint.Domain;
using FestivalSellpoint.Network.DTO;
using FestivalSellpoint.Network.ObjectProtocol;
using FestivalSellpoint.Service;
using FestivalSellpoint.Service.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FestivalSellpoint.Network.Client
{
    public class ServiceObjectProxy : ServiceObjectProxyBase, IAppService
    {        
        public ServiceObjectProxy(string host, int port) : base(host, port) { }

        public IEnumerable<Spectacol> FilterSpectacole(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Spectacol> FilterSpectacole(DateTime day)
        {
            TestConnectionOpen();
            SendRequest(new FilterSpectacoleRequest(day));
            var resp = AwaitResponse<FilteredSpectacoleResponse>();
            return resp.Spectacole.Select(s => s.ToSpectacol());
        }

        public IEnumerable<Spectacol> GetAllSpectacole()
        {
            TestConnectionOpen();
            SendRequest(new GetAllSpectacoleRequest());
            var resp = AwaitResponse<GetAllSpectacoleResponse>();
            return resp.Spectacole.Select(s => s.ToSpectacol());
        }

        public Angajat LoginAngajat(string username, string password, IObserver client)
        {
            InitializeConnection();
            Client = client;
            SendRequest(new LoginAngajatRequest(username, password));
            try
            {
                var resp = AwaitResponse<LoginAngajatResponse>();
                return resp.Angajat.ToAngajat();
            }
            catch(Exception e)
            {
                CloseConnection();
                throw new ProxyException(e);
            }
        }

        public void RegisterAngajat(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public void ReserveBilet(Spectacol spectacol, string cumparatorName, int seats)
        {
            TestConnectionOpen();
            SendRequest(new ReserveBiletRequest(new BiletDTO(cumparatorName, seats, spectacol)));
            AwaitResponse<ReserveBiletResponse>();
        }

        protected override void HandleUpdate(UpdateResponse update)
        {
            try
            {
                Client.UpdateSpectacol(update.Spectacol.ToSpectacol());                
            }
            catch (Exception e)
            {
                throw new ProxyException(e);
            }
        }

        private R AwaitResponse<R>() where R : class, IResponse
        {
            var resp = ReadResponse();
            if (resp is ErrorResponse)
                throw new ErrorResponseException((resp as ErrorResponse).Message);
            if (!(resp is R))
                throw new ProxyException($"Wrong response: expected {typeof(R).Name}, received {resp.GetType().Name}");
            return resp as R;
        }
    }
}
