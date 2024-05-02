using System;
using System.Runtime.Serialization;

namespace FestivalSellpoint.Network.Client
{
    [Serializable]
    internal class ProxyException : Exception
    {
        private Exception e;

        public ProxyException()
        {
        }

        public ProxyException(Exception e)
        {
            this.e = e;
        }

        public ProxyException(string message) : base(message)
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