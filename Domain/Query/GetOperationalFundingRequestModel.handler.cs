using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MoE.ECE.Domain.Infrastructure.Extensions;
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
                .OrderByDescending(request => request.Id)
                .ToListAsync(cancellationToken);

            if (operationalFundingRequests.Count == 0) return new List<OperationalFundingRequestModel>();

            var operationalFundingRequestModels = _mapper.MapToList<OperationalFundingRequestModel>(operationalFundingRequests);

            foreach (var operationalFundingRequestModel in operationalFundingRequestModels)
            {
                GetMatchingAdvanceMonthFundingComponents(operationalFundingRequestModel);
            }

            return operationalFundingRequestModels;
        }

        private void GetMatchingAdvanceMonthFundingComponents(
            OperationalFundingRequestModel model)
        {
            var advanceMonths = new List<AdvanceMonthFundingComponentModel>();

            if (model.EntitlementMonths != null)
            {
                foreach (var entitlementMonth in model.EntitlementMonths)
                {
                    // TODO: CAN WE MAKE PUSH THIS INTO THE DOMAIN SO WE CAN REUSE THIS LOGIC ELSEWHERE
                    var matchingAdvanceMonth =
                        _documentSession.Query<OperationalFundingRequest>()
                            .GetEntitlementAdvancedMonth(
                                model.OrganisationId, entitlementMonth.Year, entitlementMonth.MonthNumber);

                    if (matchingAdvanceMonth != null)
                        advanceMonths.Add(_mapper.Map<AdvanceMonthFundingComponentModel>(matchingAdvanceMonth));
                }
            }

            model.MatchingAdvanceMonths = advanceMonths;
        }
    }
}