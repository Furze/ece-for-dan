using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace MoE.ECE.Web.Infrastructure
{
    /// <summary>
    ///     Mediatr Behaviour for validation commands.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class FluentValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        private readonly IValidatorFactory _validatorFactory;

        public FluentValidationBehaviour(IValidatorFactory validatorFactory) => _validatorFactory = validatorFactory;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            IValidator<TRequest>? validatorForRequest = _validatorFactory.GetValidator<TRequest>();

            if (validatorForRequest != null)
            {
                ValidationResult? validationResult =
                    await validatorForRequest.ValidateAsync(request, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }

            TResponse result = await next();

            return result;
        }
    }
}