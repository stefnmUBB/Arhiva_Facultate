using FestivalSellpoint.Network.ObjectProtocol;
using FestivalSellpoint.Network.Utils;
using FestivalSellpoint.Service.Observer;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace FestivalSellpoint.Network.Client
{
    public abstract class ServiceObjectProxyBase
    {
        private string Host;
        private int Port;

        protected IObserver Client;

        private NetworkStream Stream;
        private IFormatter Formatter;
        private TcpClient Connection;

        private Queue<IResponse> Responses = new Queue<IResponse>();
        private volatile bool IsFinished = false;

        private EventWaitHandle WaitHandle;


        public ServiceObjectProxyBase(string host, int port)
        {
            Host = host;
            Port = port;
        }

        protected void CloseConnection()
        {
            IsFinished = true;
            try
            {
                Stream.Close();
                Connection.Close();
                WaitHandle.Close();
                Client = null;
            }
            catch (Exception e)
            {
                throw new ProxyException(e);
            }
        }

        byte[] IntToByteArray(int value)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(value & 0xFF); value >>= 8;
            result[1] = (byte)(value & 0xFF); value >>= 8;
            result[2] = (byte)(value & 0xFF); value >>= 8;
            result[3] = (byte)(value & 0xFF);
            return result;
        }

        private int ByteArrayToInt(byte[] arr)
        {
            return (arr[0] | (arr[1] << 8) | (arr[2] << 16) | (arr[3] << 24));
        }

        private void WriteInt(int value)
        {
            byte[] buff = IntToByteArray(value);
            Stream.Write(buff, 0, 4);            
        }

        private int ReadInt()
        {
            byte[] buff = new byte[4];
            Stream.Read(buff, 0, 4);
            return ByteArrayToInt(buff);
        }
        

        private void WriteString(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            WriteInt(buffer.Length);            
            Stream.Write(buffer, 0, buffer.Length);            
        }

        private string ReadString()
        {
            int len = ReadInt();
            byte[] buffer = new byte[len];
            Stream.Read(buffer, 0, len);
            var str = Encoding.UTF8.GetString(buffer);
            Console.WriteLine($"{len} {str}");
            return str;
        }

        protected void SendRequest(IRequest request)
        {
            try
            {
                Console.WriteLine($"Sending {Stringifier.Encode(request)}");
                WriteString(Stringifier.Encode(request));
                //Formatter.Serialize(Stream, request);
                Stream.Flush();
            }
            catch (Exception e)
            {
                throw new ProxyException("Error sending object " + e);
            }
        }

        protected IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                WaitHandle.WaitOne();
                lock (Responses) 
                {                    
                    response = Responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                throw new ProxyException(e);                
            }
            return response;
        }

        protected void InitializeConnection()
        {
            try
            {
                Connection = new TcpClient(Host, Port);
                Stream = Connection.GetStream();
                Formatter = new BinaryFormatter();
                IsFinished = false;
                WaitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                throw new ProxyException(e);
            }
        }

        private void StartReader()
        {
            new Thread(Run).Start();            
        }

        protected abstract void HandleUpdate(UpdatedSpectacolResponse update);

        public virtual void Run()
        {
            while (!IsFinished) 
            {
                //try
                {
                    var respStr = ReadString();
                    Console.WriteLine("Response = " + respStr);
                    IResponse response = Stringifier.Decode<IResponse>(respStr);                    
                    //object response = Formatter.Deserialize(Stream);
                    Console.WriteLine($"Response Received : {response}");
                    if (response is UpdatedSpectacolResponse)
                    {                        
                        HandleUpdate(response as UpdatedSpectacolResponse);
                    }
                    else
                    {
                        lock (Responses)
                            Responses.Enqueue(response as IResponse);                        
                        WaitHandle.Set();
                    }
                }
                /*catch (Exception e)
                {
                    ExceptionThrown?.Invoke(e);
                    //throw new ProxyException(e);                    
                }*/
            }
        }

        public delegate void OnExceptionThrown(Exception e);
        public event OnExceptionThrown ExceptionThrown;

        protected void TestConnectionOpen()
        {
            if (Connection == null)
                throw new ProxyException("Connection is not open");            
        }

    }
}
