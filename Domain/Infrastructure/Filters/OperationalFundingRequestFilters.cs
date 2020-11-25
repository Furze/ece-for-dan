using System.Linq;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Infrastructure.Filters
{
    public static class OperationalFundingRequestFilters
    {
        public static AdvanceMonthFundingComponent? GetEntitlementAdvancedMonth(this IQueryable<OperationalFundingRequest> fundingRequests,
            int organisationId, 
            int? entitlementYear, 
            int? monthNumber)
        {
            //TODO : WE NEED TO MOVE THIS TO THE DOMAIN SO WE DON'T HAVE TO LOOK IT UP CURRENTLY OUR DOMAIN EVENT DOES NOT HAVE
            // THIS POPULATED.
            var result = fundingRequests
                .Where(funding => funding.OrganisationId == organisationId)
                .OrderByDescending(funding => funding.RevisionNumber)
                .SelectMany(funding => funding.AdvanceMonths)
                .FirstOrDefault(advanceMonth => advanceMonth.Year == entitlementYear && advanceMonth.MonthNumber == monthNumber);

            return result;
        }
    }
}