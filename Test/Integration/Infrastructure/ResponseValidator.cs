using Bard;
using MoE.ECE.Web.Infrastructure.Middleware.Exceptions;
using Shouldly;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class ResponseValidator : BadRequestProvider<ErrorResponse>
    {
        public override IBadRequestProvider ForProperty(string propertyName)
        {
            Content().Errors?.ShouldContain(error => error.Property != null && error.Property.Contains(propertyName));

            return this;
        }

        public override IBadRequestProvider WithMessage(string message)
        {
            StringContent.ShouldContain(message);
            return this;
        }

        public override IBadRequestProvider WithErrorCode(string errorCode)
        {
            Content().Errors?.ShouldContain(error => error.ErrorCode == errorCode);

            return this;
        }

        public override IBadRequestProvider StartsWithMessage(string message)
        {
            StringContent.ShouldStartWith(message);
            return this;
        }

        public override IBadRequestProvider EndsWithMessage(string message)
        {
            StringContent.ShouldEndWith(message);
            return this;
        }
    }
}