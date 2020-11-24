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
    public class GetOperationalFundingRequestModelHandler : IQueryHandler<GetOperationalFundingRequestModel,
        ICollection<OperationalFundingRequestModel>>
    {
        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;

        public GetOperationalFundingRequestModelHandler(
            IDocumentSession documentSession,
            IMapper mapper)
        {
            _documentSession = documentSession;
            _mapper = mapper;
        }

        public async Task<ICollection<OperationalFundingRequestModel>> Handle(GetOperationalFundingRequestModel query,
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
                    RequestId = request.RequestId,
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
    }
}