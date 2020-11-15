using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Infrastructure.Extensions;
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
            Rs7 rs7 = await _documentSession.Query<Rs7>()
                .SingleOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

            if (rs7 == null)
            {
                throw new ResourceNotFoundException(
                    $"{nameof(Rs7Model)} with {nameof(Rs7Model.Id)} {query.Id} does not exist.");
            }

            Rs7Model? rs7Model = query.RevisionNumber.HasValue
                ? _mapper.MapFrom(rs7, rs7.GetRevision(query.RevisionNumber.Value)).Into<Rs7Model>()
                : _mapper.Map<Rs7Model>(rs7);

            return rs7Model;
        }
    }
}