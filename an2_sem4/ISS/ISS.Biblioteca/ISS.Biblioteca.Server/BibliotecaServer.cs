using ISS.Biblioteca.Commons.Networking.Client;
using ISS.Biblioteca.Commons.Networking.Server;
using ISS.Biblioteca.Commons.Service;
using System;
using System.Net.Sockets;
using System.Threading;

namespace ISS.Biblioteca.Server
{
    internal class BibliotecaServer : ConcurrentServer
    {
        private readonly IService _Server;      

        public BibliotecaServer(string host, int port, IService server) : base(host, port)
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
