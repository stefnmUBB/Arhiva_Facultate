using System;
using System.Runtime.Serialization;

namespace Lab3.Lexic
{
    [Serializable]
    public class LexicalErrorException : Exception
    {
        public LexicalErrorException()
        {
        }

        public LexicalErrorException(string message) : base(message)
        {
        }

        public LexicalErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LexicalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}