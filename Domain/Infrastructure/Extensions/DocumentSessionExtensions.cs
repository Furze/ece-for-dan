using System;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using MoE.ECE.Domain.Model.Rs7;
using static MoE.ECE.Domain.Exceptions.DomainExceptions;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class DocumentSessionExtensions
    {
        public static async Task<Rs7> LoadRs7Async(this IDocumentSession documentSession, int rs7Id, CancellationToken cancellationToken)
        {
            var rs7 = await documentSession.LoadAsync<Rs7>(rs7Id, cancellationToken);

            if (rs7 == null)
                throw ResourceNotFoundException<Rs7>(rs7Id);

            return rs7;
        }
        
        public static async Task<Rs7> LoadRs7ByBusinessEntityIdAsync(this IDocumentSession documentSession, Guid businessEntityId, CancellationToken cancellationToken)
        {
            var rs7 = await documentSession.Query<Rs7>()
                .SingleOrDefaultAsync(entity => entity.BusinessEntityId == businessEntityId, cancellationToken);
               
            if (rs7 == null)
                throw ResourceNotFoundException<Rs7>(businessEntityId);

            return rs7;
        }
        
        
    }
}