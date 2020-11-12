using System.Linq;
using FluentValidation;
using Marten;
using MoE.ECE.Domain.Command.Validation;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateFullRs7Validator : AbstractValidator<CreateFullRs7>
    {
        public CreateFullRs7Validator(IDocumentSession documentSession, ReferenceDataContext referenceDataContext)
        {
            RuleFor(command => command.FundingPeriod)
                .SetValidator(new ShouldBeAValidNullableEnum<FundingPeriodMonth>())
                .WithErrorCode("InvalidFundingPeriod")
                .NotEmpty();

            RuleFor(command => command.OrganisationId)
                .NotEmpty()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(command => command.EntitlementMonths)
                .NotEmpty()
                .When(command => command.AdvanceMonths == null || !command.AdvanceMonths.Any())
                .WithErrorCode("MissingEntitlementMonths")
                .WithMessage("Must have at least one entitlement month when no advance months specified.");

            RuleForEach(command => command.AdvanceMonths)
                .SetValidator(command => new AdvancedMonthValidator(command.Id, documentSession, referenceDataContext));
        }
    }
}