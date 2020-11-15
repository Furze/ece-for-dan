using FluentValidation;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementDayModelValidator : AbstractValidator<Rs7EntitlementDayModel>
    {
        public Rs7EntitlementDayModelValidator()
        {
            RuleFor(model => model.Under2)
                .InclusiveBetween(0, 9999);

            RuleFor(model => model.TwoAndOver)
                .InclusiveBetween(0, 9999);

            RuleFor(model => model.Hours20)
                .InclusiveBetween(0, 9999);

            RuleFor(model => model.Plus10)
                .InclusiveBetween(0, 9999);

            RuleFor(model => model.Certificated)
                .InclusiveBetween(0, 9999);

            RuleFor(model => model.NonCertificated)
                .InclusiveBetween(0, 9999);
        }
    }
}