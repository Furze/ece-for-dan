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
    public class GetRs7ByIdRevisionNumberHandler : IQueryHandler<GetRs7ByIdRevisionNumber, Rs7Model>
    {
        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;

        public GetRs7ByIdRevisionNumberHandler(IDocumentSession documentSession, IMapper mapper)
        {
            _documentSession = documentSession;
            _mapper = mapper;
        }

        public async Task<Rs7Model> Handle(GetRs7ByIdRevisionNumber query, CancellationToken cancellationToken)
        {
            Rs7 rs7;

            if (query.RevisionNumber.HasValue)
            {
                rs7 = await _documentSession.Query<Rs7>().FirstOrDefaultAsync(
                    r => r.Id == query.Id
                    && r.CurrentRevision.RevisionNumber == query.RevisionNumber,
                    cancellationToken);
            }
            else
            {
                rs7 = await _documentSession.Query<Rs7>()
                    .Where(r => r.Id == query.Id)
                    .OrderByDescending(r => r.CurrentRevision.RevisionNumber)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            if (rs7 == null)
            {
                throw new ResourceNotFoundException($"{nameof(Rs7Model)} with {nameof(Rs7Model.Id)} {query.Id} does not exist.");
            }

            var rs7Model = _mapper.Map<Rs7Model>(rs7);

            return rs7Model;
        }
    }
}