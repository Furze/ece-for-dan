using FluentValidation;
using Marten;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class SubmitRs7ForApprovalValidator : AbstractValidator<SubmitRs7ForApproval>
    {
        public SubmitRs7ForApprovalValidator(IDocumentSession documentSession,
            ReferenceDataContext referenceDataContext)
        {
            Include(new Rs7ModelAdvanceMonthsValidator(documentSession, referenceDataContext));
            Include(new Rs7ModelEntitlementMonthsValidator());
            Include(new Rs7ModelIsAttestedValidator(documentSession, referenceDataContext));
        }
    }
}