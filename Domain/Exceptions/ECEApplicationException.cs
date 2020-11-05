using System;
using System.Runtime.Serialization;

namespace MoE.ECE.Domain.Exceptions
{
    [Serializable]
    public class ECEApplicationException : Exception
    {
        protected ECEApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ECEApplicationException(string error) : base(error)
        {
        }
    }
}