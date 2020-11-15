using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MoE.ECE.Web.Infrastructure.Validation
{
    /// <summary>
    ///     Intercepts the construction of fluent validation error results.
    /// </summary>
    /// <remarks>
    ///     This approach is endorsed by the writer of fluent validation, as per the link below:
    ///     https://github.com/JeremySkinner/FluentValidation/issues/885#issuecomment-416996708.
    /// </remarks>
    public class UseErrorCodeInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(
            ControllerContext controllerContext,
            IValidationContext commonContext,
            ValidationResult result)
        {
            // We need to include the error code somehow, so we shoehorn it into the standard mvc validation
            // infrastructure by constructing the error message in the format of: {errorcode}|{errormessage}
            IEnumerable<ValidationFailure>? projection = result
                .Errors
                .Select(failure =>
                    new ValidationFailure(failure.PropertyName, $"{failure.ErrorCode}|{failure.ErrorMessage}"));

            return new ValidationResult(projection);
        }

        public IValidationContext BeforeMvcValidation(
            ControllerContext controllerContext,
            IValidationContext commonContext) =>
            commonContext;
    }
}