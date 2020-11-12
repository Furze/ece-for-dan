using System.Linq;
using FluentValidation;
using Marten;
using Microsoft.EntityFrameworkCore;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class AdvancedMonthValidator : AbstractValidator<Rs7AdvanceMonthModel>
    {
        public AdvancedMonthValidator()
        {
            RuleFor(model => model.MonthNumber)
                .InclusiveBetween(1, 12);

            RuleFor(model => model.Year)
                .InclusiveBetween(1, 9999);
        }

        public AdvancedMonthValidator(int rs7Id, IDocumentSession documentSession, ReferenceDataContext referenceDataContext)
            : this()
        {
            When(MonthAndYearAreValid, () =>
            {
                var rs7 = documentSession.Load<Model.Rs7.Rs7>(rs7Id);

                if (rs7 == null)
                {
                    return;
                }

                var organisation = referenceDataContext.EceServices
                    .Include(service => service.OperatingSessions)
                    .SingleOrDefault(service => service.RefOrganisationId == rs7.OrganisationId);

                if (organisation == null)
                {        
                    return;
                }

                When(model => model.ParentLed.HasValue, () =>
                {
                    RuleFor(model => model.ParentLed)
                        .GreaterThanOrEqualTo(0)
                        .LessThanOrEqualTo(model => organisation.ParentLedMaxFundingDays(model.MonthNumber, model.Year));
                });

                When(model => model.Sessional.HasValue, () =>
                {
                    RuleFor(model => model.Sessional)
                        .GreaterThanOrEqualTo(0)
                        .LessThanOrEqualTo(model => organisation.SessionalMaxFundingDays(model.MonthNumber, model.Year));
                });

                When(model => model.AllDay.HasValue, () =>
                {
                    RuleFor(model => model.AllDay)
                        .GreaterThanOrEqualTo(0)
                        .LessThanOrEqualTo(model => organisation.AllDaysMaxFundingDays(model.MonthNumber, model.Year));
                });
            });
        }

        private static bool MonthAndYearAreValid(Rs7AdvanceMonthModel model)
        {
            var isValid = model.MonthNumber > 0 && model.MonthNumber <= 12 && model.Year > 0 && model.Year <= 9999;

            return isValid;
        }
    }
}