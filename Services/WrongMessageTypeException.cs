using System;
using System.Runtime.Serialization;

namespace Labs.Services.IO
{
    [Serializable]
    internal class WrongMessageTypeException : Exception
    {
        public WrongMessageTypeException()
        {
        }

        public WrongMessageTypeException(string message) : base(message)
        {
        }

        public WrongMessageTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongMessageTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}