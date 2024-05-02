using FestivalSellpoint.Domain;
using FestivalSellpoint.Network.DTO;
using FestivalSellpoint.Network.ObjectProtocol;
using FestivalSellpoint.Service;
using FestivalSellpoint.Service.Observer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Network.Client
{
    public class ClientObjectWorker : ClientObjectWorkerBase, IObserver
    {
        private IAppService Server;

        public ClientObjectWorker(IAppService server, TcpClient connection) : base(connection)
        {
            Server = server;
        }

        public void UpdateSpectacol(Spectacol s)
        {
            Console.WriteLine("Worker: Updated spectacol " + s);
            var sDto = SpectacolDTO.FromSpectacol(s);
            try
            {
                SendResponse(new UpdatedSpectacolResponse(sDto));
            }
            catch (IOException e)
            {
                throw new ServerProcessingException(e);                
            }
        }

        protected override IResponse HandleRequest(IRequest request)
        {            
            if (request is GetAllSpectacoleRequest)
            {
                return ResponseOrError(() => new GetAllSpectacoleResponse(Server.GetAllSpectacole().ToArray()));                
            }
            if(request is FilterSpectacoleRequest)
            {
                return ResponseOrError(()
                    => new FilterSpectacoleResponse(Server
                        .FilterSpectacole((request as FilterSpectacoleRequest).Day)
                        .ToArray()));
            }
            if(request is LoginAngajatRequest)
            {
                var username = (request as LoginAngajatRequest).Username;
                var password = (request as LoginAngajatRequest).Password;
                return ResponseOrError(() =>
                {
                    var angajat = Server.LoginAngajat(username, password, this);
                    return new LoginAngajatResponse(AngajatDTO.FromAngajat(angajat));
                });
            }
            if(request is ReserveBiletRequest)
            {
                var bilet = (request as ReserveBiletRequest).Bilet;
                var spectacol = bilet.Spectacol.ToSpectacol();
                var cumparator = bilet.NumeCumparator;
                var nrLocuri = bilet.NrLocuri;

                Console.WriteLine($"Server is {Server.GetType().Name}");

                return ResponseOrError(() =>
                {
                    Server.ReserveBilet(spectacol, cumparator, nrLocuri);
                    return new ReserveBiletResponse();
                });
            }
            return new ErrorResponse("No Response");
        }

        private static IResponse ResponseOrError(Func<IResponse> getResponse)
        {
            try
            {
                return getResponse();
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }
    }
}
