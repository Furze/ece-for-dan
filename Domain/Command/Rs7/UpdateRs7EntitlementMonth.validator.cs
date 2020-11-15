using FluentValidation;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class UpdateRs7EntitlementMonthValidator : AbstractValidator<UpdateRs7EntitlementMonth>
    {
        public UpdateRs7EntitlementMonthValidator() => Include(new Rs7EntitlementMonthModelValidator());
    }
}