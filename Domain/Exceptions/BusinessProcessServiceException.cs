using System;
using System.Runtime.Serialization;

namespace MoE.ECE.Domain.Exceptions
{
    [Serializable]
    public class BusinessProcessServiceException : Exception
    {
        protected BusinessProcessServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BusinessProcessServiceException(string error) : base(error)
        {
        }
    }
}