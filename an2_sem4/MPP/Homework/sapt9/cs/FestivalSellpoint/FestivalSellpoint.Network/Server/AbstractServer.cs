using System;
using System.Net;
using System.Net.Sockets;

namespace FestivalSellpoint.Network.Server
{
    public abstract class AbstractServer
    {
        private TcpListener Server;
        private string Host;
        private int Port;

        public AbstractServer(string host, int port)
        {            
            Host = host;
            Port = port;
        }

        public void Start()
        {
            var adr = IPAddress.Parse(Host);
            var endpoint = new IPEndPoint(adr, Port);
            Server = new TcpListener(endpoint);
            Server.Start();
            while(true)
            {
                Console.WriteLine("Waiting for clients...");
                var client = Server.AcceptTcpClient();
                Console.WriteLine("Client connected...");
                ProcessRequest(client);
            }
        }

        public abstract void ProcessRequest(TcpClient client);
    }
}
