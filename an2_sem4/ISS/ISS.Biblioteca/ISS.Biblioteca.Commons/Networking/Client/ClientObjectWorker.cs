using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Networking.Requests;
using ISS.Biblioteca.Commons.Networking.Responses;
using ISS.Biblioteca.Commons.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Networking.Client
{
    public class ClientObjectWorker : ClientObjectWorkerBase, IClientObserver
    {
        private IService Server;

        public ClientObjectWorker(IService server, TcpClient connection) : base(connection)
        {
            Server = server;
        }

        public void OnImprumut(Imprumut imprumut)
        {
            SendResponse(new UpdateExemplarImprumutatResponse(imprumut));
        }

        public void OnRetur(Retur retur)
        {
            SendResponse(new UpdateExemplarReturnatResponse(retur));
        }

        protected override IResponse HandleRequest(IRequest request)
        {
            if (request is GetCartiDisponibileRequest) 
            {
                return ResponseOrError(() => new GetCartiListReponse(Server.GetCartiDisponibile().ToArray()));
            }
            if (request is GetCartiRequest)
            {
                return ResponseOrError(() => new GetCartiListReponse(Server.GetCarti().ToArray()));
            }
            if (request is LoginRequest loginRequest)
            {
                var cnp = loginRequest.Cnp;
                var token = loginRequest.Token;
                return ResponseOrError(() => new LoginResponse(Server.Login(cnp, token, this)));                
            }
            if(request is RegisterRequest registerRequest)
            {
                var user = registerRequest.Utilizator;
                return ResponseOrError(() => new RegisterResponse(Server.Register(user)));
            }
            if(request is GetImprumuturiByAbonatRequest imprumuturiRequest)
            {
                var abonat = imprumuturiRequest.Abonat;
                return ResponseOrError(() => new ImprumuturiByAbonatResponse(Server.GetImprumuturiByAbonat(abonat)));
            }
            if(request is GetDisponibleExemplarOfRequest exemplarRequest)
            {
                var carte = exemplarRequest.Carte;
                return ResponseOrError(() => new ExemplarResponse(Server.GetDisponibleExemplarOf(carte)));
            }
            if(request is EfectueazaImprumutRequest imprumutRequest)
            {
                var abonat = imprumutRequest.Abonat;
                var exemplar = imprumutRequest.Exemplar;
                return ResponseOrError(() => new ImprumutResponse(Server.EfectueazaImprumut(abonat, exemplar)));
            }
            if(request is GetAbonatByCnpRequest abonatRequest)
            {
                return ResponseOrError(() => new AbonatResponse(Server.GetAbonatByCnp(abonatRequest.Cnp)));
            }
            if(request is ReturRequest returRequest)
            {
                var bibliotecar = returRequest.Bibliotecar;
                var imprumut = returRequest.Imprumut;
                return ResponseOrError(() => new ReturResponse(Server.EfectueazaRetur(bibliotecar, imprumut)));
            }
            return new ErrorResponse("No Response");
        }        
    }
}
