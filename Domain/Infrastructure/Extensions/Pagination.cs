using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoE.ECE.Domain.Query;
using MoE.ECE.Domain.Read.Model;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class Pagination
    {
        public static async Task<CollectionModel<TReadModel>> Paginate<TReadModel>(
            this IQueryable<TReadModel> query,
            PaginationParameters parameters,
            CancellationToken cancellationToken)
        {
            int count = await query.CountAsync(cancellationToken);

            IQueryable<TReadModel>? paginationQuery = query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            TReadModel[]? data = await paginationQuery.ToArrayAsync(cancellationToken);

            return new CollectionModel<TReadModel>(
                parameters.PageSize,
                parameters.PageNumber,
                count, data);
        }
    }
}