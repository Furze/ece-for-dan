using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MoE.ECE.Domain.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        private const string BadRequestExceptionErrorCode = "BadRequestException.ErrorCode";

        public BadRequestException(
            string errorCode,
            string message,
            Exception? innerException = null)
            : base(message, innerException) =>
            ErrorCode = errorCode;

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) =>
            ErrorCode = info.GetString(BadRequestExceptionErrorCode) ?? string.Empty;

        public string ErrorCode { get; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(BadRequestExceptionErrorCode, ErrorCode);
        }
    }
}