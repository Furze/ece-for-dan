using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoE.ECE.Domain.Infrastructure.EntityFramework
{
    public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T>, IEntityConfiguration where T : class
    {
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

        /// <summary>
        /// Provides the hook when this configuration is applied to setup the entity.
        /// </summary>
        void IEntityTypeConfiguration<T>.Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureEntity(builder);
        }
    }
}