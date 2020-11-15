using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MoE.ECE.Domain.Exceptions;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    public class BadRequestExceptionMiddleware : ExceptionMiddlewareBase<BadRequestException>
    {
        public BadRequestExceptionMiddleware(
            RequestDelegate next,
            ILogger<BadRequestExceptionMiddleware> logger)
            : base(next, logger)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        protected override ErrorResponse CreateResponse(HttpContext context, BadRequestException exception) =>
            new {Errors = new[] {new Error {ErrorCode = exception.ErrorCode, Message = CreateMessage(exception)}}};
    }
}