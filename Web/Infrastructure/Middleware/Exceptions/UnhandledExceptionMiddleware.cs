using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    public class UnhandledExceptionMiddleware : ExceptionMiddlewareBase<Exception>
    {
        public UnhandledExceptionMiddleware(
            RequestDelegate next,
            ILogger<UnhandledExceptionMiddleware> logger)
            : base(next, logger)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        protected override string CreateMessage(Exception ex) => "An unhandled error occurred";
    }
}