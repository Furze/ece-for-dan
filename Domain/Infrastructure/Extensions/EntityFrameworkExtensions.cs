using Microsoft.EntityFrameworkCore;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        ///     Wraps the standard Find, but with correct nullable signature for clearer usage
        /// </summary>
        public static TEntity? FindNullable<TEntity>(this DbSet<TEntity> dbSet, params object[] keyValues)
            where TEntity : class
        {
            TEntity? result = dbSet.Find(keyValues);
            return result;
        }
    }
}