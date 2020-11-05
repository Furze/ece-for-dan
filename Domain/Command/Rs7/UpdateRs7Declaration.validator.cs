using FluentValidation;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class UpdateRs7DeclarationValidator : AbstractValidator<UpdateRs7Declaration>
    {
        public UpdateRs7DeclarationValidator()
        {
            Include(new DeclarationModelValidator(true));
            RuleFor(x => x.IsDeclaredTrue).Equal(true);
        }
    }
}