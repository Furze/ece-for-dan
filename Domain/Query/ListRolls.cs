using MoE.ECE.Domain.Read.Model;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class ListRolls : PaginationParameters, IQuery<CollectionModel<RollModel>>
    {
        public int OrganisationId { get; }

        public ListRolls(int organisationId)
        {
            OrganisationId = organisationId;
        }
    }
}