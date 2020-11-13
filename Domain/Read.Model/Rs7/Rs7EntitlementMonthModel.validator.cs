using FluentValidation;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementMonthModelValidator : AbstractValidator<Rs7EntitlementMonthModel>
    {
        public Rs7EntitlementMonthModelValidator()
        {
            RuleForEach(model => model.Days)
                .SetValidator(new Rs7EntitlementDayModelValidator());
        }
    }
}