using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Services;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class FindServices : PaginationParameters, IQuery<CollectionModel<SearchEceServiceModel>>
    {
        public FindServices(string searchTerm)
        {
            SearchTerm = searchTerm;
        }

        public string SearchTerm { get; }
    }
}