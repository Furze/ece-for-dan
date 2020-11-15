using FluentValidation;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class DeclarationModelValidator : AbstractValidator<DeclarationModel?>
    {
        public DeclarationModelValidator()
        {
            When(model => model != null && model.FullName != string.Empty, () =>
            {
                RuleFor(model => model!.FullName)
                    .Length(2, 150);
            });

            When(model => model != null && model.ContactPhone != string.Empty, () =>
            {
                RuleFor(model => model!.ContactPhone)
                    .Length(2, 50);
            });

            When(model => model != null && model.Role != string.Empty, () =>
            {
                RuleFor(model => model!.Role)
                    .Length(2, 100);
            });
        }

        public DeclarationModelValidator(bool applyFullValidation) : this() =>
            When(model => model != null && applyFullValidation, () =>
            {
                RuleFor(model => model!.FullName)
                    .NotEmpty();

                RuleFor(model => model!.ContactPhone)
                    .NotEmpty();

                RuleFor(model => model!.Role)
                    .NotEmpty();

                RuleFor(model => model!.IsDeclaredTrue)
                    .NotEmpty();
            });
    }
}