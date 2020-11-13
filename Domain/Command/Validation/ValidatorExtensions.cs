using FluentValidation;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Command.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<TModel, int?> MustBeAValidOrganisation<TModel>(this IRuleBuilderOptions<TModel, int?> ruleBuilder, ReferenceDataContext referenceDataContext)
        {
            return ruleBuilder
                .Must((model, organisationId) => IsAValidOrganisation(organisationId, referenceDataContext))
                .WithMessage("{PropertyName} is not valid."); 
        }
        
        private static bool IsAValidOrganisation(int? organisationId, ReferenceDataContext referenceDataContext)
        {
            if (organisationId == null)
                return true;

            var organisation = referenceDataContext.EceServices.Find(organisationId.Value);

            return organisation != null;
        }
    }
}