using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetRs7ByBusinessEntityIdHandler : IQueryHandler<GetRs7ByBusinessEntityId, Rs7Model>
    {
        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;

        public GetRs7ByBusinessEntityIdHandler(IDocumentSession documentSession, IMapper mapper)
        {
            _documentSession = documentSession;
            _mapper = mapper;
        }

        public async Task<Rs7Model> Handle(GetRs7ByBusinessEntityId query, CancellationToken cancellationToken)
        {
            Rs7? rs7 = await _documentSession.Query<Rs7>()
                .Where(r => r.BusinessEntityId == query.BusinessEntityId)
                .OrderByDescending(r => r.CurrentRevision.RevisionNumber)
                .FirstOrDefaultAsync(cancellationToken);

            if (rs7 == null)
            {
                throw new ResourceNotFoundException(
                    $"{nameof(Rs7Model)} with {nameof(Rs7Model.BusinessEntityId)} {query.BusinessEntityId} does not exist.");
            }

            Rs7Model? rs7Model = _mapper.Map<Rs7Model>(rs7);

            return rs7Model;
        }
    }
}