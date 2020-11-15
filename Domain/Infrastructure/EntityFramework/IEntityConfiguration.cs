using Microsoft.EntityFrameworkCore;

namespace MoE.ECE.Domain.Infrastructure.EntityFramework
{
    /// <summary>
    ///     Allows configuration for an entity type to be factored into a separate class,
    ///     in a non-generic manner which lends itself to having configurations discovered
    ///     and injected into a db context.
    /// </summary>
    public interface IEntityConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}