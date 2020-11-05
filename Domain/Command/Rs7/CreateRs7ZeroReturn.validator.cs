using FluentValidation;
using MoE.ECE.Domain.Command.Validation;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateRs7ZeroReturnValidator : AbstractValidator<CreateRs7ZeroReturn>
    {
        public CreateRs7ZeroReturnValidator(ReferenceDataContext referenceDataContext)
        {
            RuleFor(command => command.FundingPeriod)
                .SetValidator(new ShouldBeAValidNullableEnum<FundingPeriodMonth>())
                .NotEmpty();

            RuleFor(command => command.FundingPeriodYear)
                .NotEmpty();

            RuleFor(command => command.OrganisationId)
                .NotEmpty()
                .MustBeAValidOrganisation(referenceDataContext);
        }
    }
}