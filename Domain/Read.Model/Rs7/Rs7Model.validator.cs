using System.Linq;
using FluentValidation;
using Marten;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7ModelAdvanceMonthsValidator : AbstractValidator<IRs7AdvanceMonths>
    {
        public Rs7ModelAdvanceMonthsValidator(IDocumentSession documentSession, ReferenceDataContext referenceDataContext)
        {
            RuleFor(command => command.AdvanceMonths)
                .Must((command, months) => months?.Count() == 4)
                .When(command => command.AdvanceMonths != null)
                .WithErrorCode("AdvanceMonthsMissing")
                .WithMessage("A total of 4 months must be provided.")
                .NotEmpty();

            RuleForEach(command => command.AdvanceMonths)
                .SetValidator(command => new AdvancedMonthValidator(command.Id, documentSession, referenceDataContext));
        }
    }
    
    public class Rs7ModelEntitlementMonthsValidator : AbstractValidator<IRs7EntitlementMonths>
    {
        public Rs7ModelEntitlementMonthsValidator()
        {
            RuleFor(command => command.EntitlementMonths)
                .Must((command, months) => months?.Count() == 4)
                .When(command => command.EntitlementMonths != null)
                .WithErrorCode("EntitlementMonthsMissing")
                .WithMessage("A total of 4 months must be provided.")
                .NotEmpty();

            RuleForEach(command => command.EntitlementMonths)
                .SetValidator(new Rs7EntitlementMonthModelValidator());
        }
    }
    
    public class Rs7ModelIsAttestedValidator : AbstractValidator<IRs7Attestation>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ReferenceDataContext _referenceDataContext;

        public Rs7ModelIsAttestedValidator(IDocumentSession documentSession, ReferenceDataContext referenceDataContext)
        {
            _documentSession = documentSession;
            _referenceDataContext = referenceDataContext;
            
            RuleFor(command => command.IsAttested)
                .Must(IsRequiredForCertainOrganisationTypes)
                .WithMessage("'Is Attested' must not be empty.");
        }
        
        private bool IsRequiredForCertainOrganisationTypes(IRs7Attestation updateRs7Model, bool? isAttested)
        {
            var rs7 = _documentSession.Load<Domain.Model.Rs7.Rs7>(updateRs7Model.Id);

            if (rs7 == null) return true;

            var organisation =
                _referenceDataContext.EceServices.Find(rs7.OrganisationId);

            if (organisation == null)
                return true;

            return !organisation.IsAttestationRequired || isAttested.HasValue;
        }
    }
}