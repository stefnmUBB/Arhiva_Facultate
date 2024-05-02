using System;
using System.Runtime.Serialization;

namespace ISS.Biblioteca.Commons.Networking.Client
{
    [Serializable]
    internal class ErrorResponseException : Exception
    {
        public ErrorResponseException()
        {
        }

        public ErrorResponseException(string message) : base(message)
        {
        }

        public ErrorResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ErrorResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}