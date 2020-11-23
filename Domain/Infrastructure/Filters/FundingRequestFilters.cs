using System.Linq;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Infrastructure.Filters
{
    public static class ReferenceDataFilters
    {
        public static IQueryable<EceServiceDateRangedParameter> GetHistory2(
            this IQueryable<EceServiceDateRangedParameter> dateRangedParams,
            int organisationId,
             params string[] attributes)
        {
            var result = dateRangedParams
                .Where(entity =>
                    entity.RefOrganisationId == organisationId &&
                    attributes.Contains(entity.Attribute));

            return result;
        }
        public static IQueryable<EceServiceDateRangedParameter> GetHistory(
            this IQueryable<EceServiceDateRangedParameter> dateRangedParams,
            int organisationId,
            string attribute)
        {
            var result = dateRangedParams
                .Where(entity =>
                        entity.RefOrganisationId == organisationId &&
                        entity.Attribute == attribute);

            return result;
        }

        public static IQueryable<EceOperatingSessionDateRangedParameter> GetOperatingSessionHistory(
            this IQueryable<EceOperatingSessionDateRangedParameter> requests,
            int organisationId,
            string attribute)
        {
            var result = requests
                .Where(entity =>
                    entity.RefOrganisationId == organisationId &&
                    entity.SessionDayDescription == attribute &&
                    entity.EceLicencingDetailDateRangedParameter.ApplicationStatusId != ApplicationStatus.Withdrawn &&
                    entity.EceLicencingDetailDateRangedParameter.ApplicationStatusId != ApplicationStatus.Overwritten &&
                    entity.EceLicencingDetailDateRangedParameter.ApplicationStatusId != ApplicationStatus.Recommended &&
                    entity.EceLicencingDetailDateRangedParameter.ApplicationStatusId != ApplicationStatus.Received);

            return result;
        }

        public static IQueryable<EceLicencingDetailDateRangedParameter> GetLicencingDetailHistory(
            this IQueryable<EceLicencingDetailDateRangedParameter> requests,
            int organisationId)
        {
            var result = requests
                .Where(entity => entity.RefOrganisationId == organisationId &&
                     entity.ApplicationStatusId != ApplicationStatus.Withdrawn &&
                     entity.ApplicationStatusId != ApplicationStatus.Overwritten &&
                     entity.ApplicationStatusId != ApplicationStatus.Recommended &&
                     entity.ApplicationStatusId != ApplicationStatus.Received);

            return result;
        }
    }
    public static class FundingRequestFilters
    {
        public static AdvanceMonthFundingComponent? GetEntitlementAdvancedMonth(this IQueryable<OperationalFundingRequest> fundingRequests,
            int organisationId, 
            int? entitlementYear, 
            int? monthNumber)
        {
            //TODO : CAN WE MOVE THIS TO THE DOMAIN?
            var result = fundingRequests
                .Where(funding => funding.OrganisationId == organisationId)
                .OrderByDescending(funding => funding.RevisionNumber)
                .SelectMany(funding => funding.AdvanceMonths)
                .Where(advanceMonth => advanceMonth.Year == entitlementYear && advanceMonth.MonthNumber == monthNumber)
                .ToList()
                .FirstOrDefault();

            return result;
        }
    }
}