using System;
using System.Runtime.Serialization;

namespace MoE.ECE.Domain.Exceptions
{
    [Serializable]
    public class BadDataException : Exception
    {
        public BadDataException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }

        protected BadDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}