using System;
using System.Runtime.Serialization;

namespace HelpingHands.API
{
    [Serializable]
    internal class RestException : Exception
    {
        public RestException()
        {
        }

        public RestException(string message) : base(message)
        {
        }

        public RestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}