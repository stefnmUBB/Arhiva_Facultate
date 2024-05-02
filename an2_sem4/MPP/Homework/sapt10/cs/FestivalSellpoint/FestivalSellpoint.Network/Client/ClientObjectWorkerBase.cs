using FestivalSellpoint.Service;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FestivalSellpoint.Network.ObjectProtocol;
using System.Threading;
using FestivalSellpoint.Network.Utils;
using FestivalSellpoint.Network.DTO;
using System.Text;

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
                    var reqStr = ReadString();
                    Console.WriteLine("Request = " + reqStr);
                    var request = Stringifier.Decode<IRequest>(reqStr);
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
            Console.WriteLine($"Sending {Stringifier.Encode(response)}");            
            
            lock (Stream)
            {
                WriteString(Stringifier.Encode(response));                
                Stream.Flush();
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
    }
}
