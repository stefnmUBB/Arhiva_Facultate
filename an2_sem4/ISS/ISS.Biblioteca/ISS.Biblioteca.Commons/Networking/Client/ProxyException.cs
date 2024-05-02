using System;
using System.Runtime.Serialization;

namespace ISS.Biblioteca.Commons.Networking.Client
{
    [Serializable]
    internal class ProxyException : Exception
    {        

        public ProxyException()
        {
        }

        public ProxyException(string message) : base(message)
        {
        }

        public ProxyException(Exception e) : base(e.Message, e)
        {
            
        }

        public ProxyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}