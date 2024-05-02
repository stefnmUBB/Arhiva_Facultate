using FestivalSellpoint.Service;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FestivalSellpoint.Network.ObjectProtocol;
using System.Threading;

namespace FestivalSellpoint.Network.Client
{
    public abstract class ClientObjectWorkerBase
    {        
        private TcpClient Connection;
        private NetworkStream Stream;
        private IFormatter Formatter;
        private volatile bool IsConnected = false;

        public ClientObjectWorkerBase(TcpClient connection)
        {            
            Connection = connection;
            try
            {
                Stream = connection.GetStream();
                Formatter = new BinaryFormatter();
                IsConnected = true;
            }
            catch (Exception e)
            {
                throw new ServerProcessingException(e);
            }
        }


        public /*virtual*/ void Run()
        {
            while(IsConnected)
            {
                try
                {                    
                    var request = Formatter.Deserialize(Stream);
                    Console.WriteLine("received request " + request);
                    var response = HandleRequest(request as IRequest);
                    if (response != null) 
                    {
                        SendResponse(response);
                    }
                }
                catch(SerializationException)
                {
                    // this happens when the stream is empty (stream.Length==0 is not an option apparently)
                    // so skip this exception
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.GetType().Name);
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("------------------------------------");
                    
                    //throw new ServerProcessingException(e);
                }

                try { Thread.Sleep(500); } catch(Exception e) { Console.WriteLine(e.StackTrace); }
            }

            try
            {
                Stream.Close();
                Connection.Close();
            }
            catch(Exception e)
            {
                throw new ServerProcessingException(e);
            }
        }

        protected abstract IResponse HandleRequest(IRequest request);        

        protected void SendResponse(IResponse response)
        {
            Console.WriteLine("sending response " + response);
            lock (Stream)
            {
                Formatter.Serialize(Stream, response);
                Stream.Flush();
            }
        }
    }
}
