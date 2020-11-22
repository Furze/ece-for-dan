using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MoE.ECE.Domain.Infrastructure.Filters;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetOperationalFundingRequestWashupHandler : IQueryHandler<GetOperationalFundingRequestWashup,
        ICollection<OperationalFundingRequestModel>>
    {
        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;

        public GetOperationalFundingRequestWashupHandler(
            IDocumentSession documentSession,
            IMapper mapper)
        {
            _documentSession = documentSession;
            _mapper = mapper;
        }

        public async Task<ICollection<OperationalFundingRequestModel>> Handle(GetOperationalFundingRequestWashup query,
            CancellationToken cancellationToken)
        {
            var operationalFundingRequestsQuery = _documentSession.Query<OperationalFundingRequest>()
                .Where(request => request.BusinessEntityId == query.BusinessEntityId);

            if (query.RevisionNumber.HasValue)
                operationalFundingRequestsQuery = operationalFundingRequestsQuery
                    .Where(request => request.RevisionNumber == query.RevisionNumber);

            var operationalFundingRequests = await operationalFundingRequestsQuery
                .ToListAsync(cancellationToken);

            if (operationalFundingRequests.Count == 0) return new List<OperationalFundingRequestModel>();

            var operationalFundingRequestModel = operationalFundingRequests
                .OrderByDescending(request => request.Id)
                .Select(request => new OperationalFundingRequestModel
                {
                    BusinessEntityId = request.BusinessEntityId,
                    RevisionNumber = request.RevisionNumber,
                    OrganisationId = request.OrganisationId,
                    FundingYear = request.FundingYear,
                    FundingPeriodMonth = request.FundingPeriodMonth,
                    TotalWashUp = request.TotalWashUp,
                    TotalAdvance = request.TotalAdvance,
                    AdvanceMonths = _mapper.Map<ICollection<AdvanceMonthFundingComponentModel>>(request.AdvanceMonths),
                    MatchingAdvanceMonths = GetMatchingAdvanceMonthFundingComponents(request),
                    EntitlementMonths =
                        _mapper.Map<ICollection<EntitlementMonthFundingComponentModel>>(request.EntitlementMonths)
                }).ToList();

            return operationalFundingRequestModel;
        }

        private ICollection<AdvanceMonthFundingComponentModel> GetMatchingAdvanceMonthFundingComponents(
            OperationalFundingRequest request)
        {
            var advanceMonths = new List<AdvanceMonthFundingComponentModel>();

            if (request.EntitlementMonths == null) return advanceMonths;

            foreach (var entitlementMonth in request.EntitlementMonths)
            {
                //TODO: CAN WE MAKE THIS MORE 
                var matchingAdvanceMonth =
                    _documentSession.Query<OperationalFundingRequest>()
                        .GetEntitlementAdvancedMonth(
                            request.OrganisationId, entitlementMonth.Year, entitlementMonth.MonthNumber);

                if (matchingAdvanceMonth != null)
                    advanceMonths.Add(_mapper.Map<AdvanceMonthFundingComponentModel>(matchingAdvanceMonth));
            }

            return advanceMonths;
        }

        // private ICollection<WashupFundingComponentModel> GetWashUpFundingComponents(
        //     EntitlementMonthFundingComponent entitlementMonth,
        //     AdvanceMonthFundingComponent? advanceMonth)
        // {
        //     var washUpFundingComponents = new List<WashupFundingComponentModel>();
        //
        //     washUpFundingComponents.AddRange(GetEntitlementFundingComponents(entitlementMonth));
        //
        //     var advancedFundingComponents = GetAdvanceFundingComponents(advanceMonth);
        //
        //     washUpFundingComponents.AddRange(advancedFundingComponents);
        //
        //     return washUpFundingComponents;
        // }

        // private List<WashupFundingComponentModel> GetEntitlementFundingComponents(
        //     EntitlementMonthFundingComponent entitlementMonth)
        // {
        //     return
        //         entitlementMonth.EntitlementFundingComponents.Select(entitlementFunding =>
        //             new WashupFundingComponentModel
        //             {
        //                 EntitlementAmount = entitlementFunding?.Amount,
        //                 EntitlementFundedChildHours = entitlementFunding?.FundedChildHours,
        //                 EntitlementFundingComponentTypeId = entitlementFunding?.FundingComponentTypeId,
        //                 EntitlementRate = entitlementFunding?.Rate,
        //                 EntitlementSessionTypeId = entitlementFunding?.SessionTypeId
        //             }).ToList();
        // }

        // private static List<WashupFundingComponentModel> GetAdvanceFundingComponents(
        //     AdvanceMonthFundingComponent? advanceMonth)
        // {
        //     var washUpFundingComponents = new List<WashupFundingComponentModel>();
        //     if (advanceMonth != null)
        //         washUpFundingComponents = advanceMonth.AdvanceFundingComponents.Select(advanceFunding =>
        //             new WashupFundingComponentModel
        //             {
        //                 AdvanceAmount = advanceFunding?.Amount,
        //                 AdvanceFundedChildHours = advanceFunding?.FundedChildHours,
        //                 AdvanceFundingComponentTypeId = advanceFunding?.FundingComponentTypeId,
        //                 AdvanceRate = advanceFunding?.Rate,
        //                 AdvanceSessionTypeId = advanceFunding?.SessionTypeId
        //             }).ToList();
        //
        //     return washUpFundingComponents;
        // }
    }
}