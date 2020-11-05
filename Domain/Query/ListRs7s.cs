using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class ListRs7S : PaginationParameters, IQuery<CollectionModel<Rs7Model>>

    {
    }
}