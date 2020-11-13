using FluentValidation;
using Marten;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class UpdateRs7Validator : AbstractValidator<UpdateRs7>
    {
        public UpdateRs7Validator(IDocumentSession rollContext, ReferenceDataContext referenceDataContext)
        {
            Include(new Rs7ModelAdvanceMonthsValidator(rollContext, referenceDataContext));
            Include(new Rs7ModelEntitlementMonthsValidator());
            Include(new Rs7ModelIsAttestedValidator(rollContext, referenceDataContext));

            RuleFor(rs7 => rs7.Declaration)
                .NotNull()
                .SetValidator(update => new DeclarationModelValidator(update.RollStatus != RollStatus.ExternalDraft));
        }
    }
}