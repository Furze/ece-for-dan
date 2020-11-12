using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MoE.ECE.Domain.Exceptions;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    public class BadDataExceptionMiddleware : ExceptionMiddlewareBase<BadDataException>
    {
        public BadDataExceptionMiddleware(
            RequestDelegate next,
            ILogger<BadRequestExceptionMiddleware> logger)
            : base(next, logger)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        protected override string CreateMessage(BadDataException exception) => "An unhandled error occurred";
    }
}