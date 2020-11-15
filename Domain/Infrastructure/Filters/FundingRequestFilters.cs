using System.Linq;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Infrastructure.Filters
{
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