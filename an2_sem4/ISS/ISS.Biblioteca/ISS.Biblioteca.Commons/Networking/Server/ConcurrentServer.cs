using System.Net.Sockets;
using System.Threading;

namespace ISS.Biblioteca.Commons.Networking.Server
{
    public abstract class ConcurrentServer : AbstractServer
    {
        public ConcurrentServer(string host, int port) : base(host, port) { }

        public override void ProcessRequest(TcpClient client)
        {
            CreateWorker(client).Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}
