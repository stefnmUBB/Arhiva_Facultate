using FestivalSellpoint.Network.Client;
using FestivalSellpoint.Network.Server;
using FestivalSellpoint.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FestivalSellpoint.Server
{
    internal class Server : ConcurrentServer
    {
        private readonly IAppService _Server;

        // WHY ?
        // private ChatClientWorker worker; 

        public Server(string host, int port, IAppService server) : base(host, port)
        {
            _Server = server;
            Console.WriteLine("Created server...");
        }
        protected override Thread CreateWorker(TcpClient client)
        {
            var Worker = new ClientObjectWorker(_Server, client);
            return new Thread(() => Worker.Run()); 
        }
    }
}
