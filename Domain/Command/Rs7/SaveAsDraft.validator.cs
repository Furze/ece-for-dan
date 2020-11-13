using FluentValidation;
using Marten;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class SaveAsDraftValidator : AbstractValidator<SaveAsDraft>
    {
        public SaveAsDraftValidator(IDocumentSession documentSession, ReferenceDataContext referenceDataContext)
        {
            Include(new Rs7ModelAdvanceMonthsValidator(documentSession, referenceDataContext));
            Include(new Rs7ModelEntitlementMonthsValidator());

            RuleFor(rs7 => rs7.Declaration)
                .SetValidator(draft => new DeclarationModelValidator(draft.RollStatus != RollStatus.ExternalDraft));
        }
    }
}