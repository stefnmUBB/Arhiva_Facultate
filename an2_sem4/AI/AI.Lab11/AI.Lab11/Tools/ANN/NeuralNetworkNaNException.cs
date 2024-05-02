using System;
using System.Runtime.Serialization;

namespace AI.Lab11.Tools.ANN
{
    [Serializable]
    internal class NeuralNetworkNaNException : Exception
    {
        public NeuralNetworkNaNException()
        {
        }

        public NeuralNetworkNaNException(string message) : base(message)
        {
        }

        public NeuralNetworkNaNException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NeuralNetworkNaNException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}