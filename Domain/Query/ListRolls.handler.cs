using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using Marten.Pagination;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model;
using Moe.ECE.Events.Integration;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class ListRollsHandler : IQueryHandler<ListRolls, CollectionModel<RollModel>>
    {
        private readonly IDocumentSession _documentSession;

        public ListRollsHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<CollectionModel<RollModel>> Handle(ListRolls query, CancellationToken cancellationToken)
        {
            var rs7S = await _documentSession.Query<Rs7>()
                .Where(rs7 => rs7.OrganisationId == query.OrganisationId)
                .Where(roll => roll.RollStatus != RollStatus.ExternalNew)
                .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            var rolls = rs7S
                .Select(rs7 => new RollModel
                {
                    Id = rs7.Id,
                    RollType = Constants.BusinessEntityTypes.Rs7,
                    BusinessEntityId = rs7.BusinessEntityId,
                    Status = rs7.RollStatus,
                    Received = rs7.ReceivedDate,
                    OrganisationId = rs7.OrganisationId,
                    FundingPeriodMonth = rs7.FundingPeriod,
                    FundingYear = rs7.FundingYear,
                    FundingPeriodYear = rs7.FundingPeriodYear
                })
                .ToArray();

            return new CollectionModel<RollModel>(query.PageSize, query.PageNumber, rolls.Length, rolls);  
        }
    }
}