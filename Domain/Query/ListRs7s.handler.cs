using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using Marten.Pagination;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class ListRs7SHandler : IQueryHandler<ListRs7S, CollectionModel<Rs7Model>>
    {
        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;

        public ListRs7SHandler(IDocumentSession documentSession, IMapper mapper)
        {
            _documentSession = documentSession;
            _mapper = mapper;
        }

        public async Task<CollectionModel<Rs7Model>> Handle(ListRs7S query,
            CancellationToken cancellationToken)
        {
            IPagedList<Rs7>? models = await _documentSession.Query<Rs7>()
                .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            Rs7Model[]? result = _mapper.Map<Rs7Model[]>(models);

            return new CollectionModel<Rs7Model>(
                query.PageSize,
                query.PageNumber,
                result.Length, result);
        }
    }
}