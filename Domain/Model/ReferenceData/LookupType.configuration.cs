using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class LookupTypeConfiguration : EntityConfigurationBase<LookupType>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<LookupType> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder
                .HasMany(entity => entity.Lookups)
                .WithOne()
                .HasForeignKey(entity => entity.LookupTypeId);

            builder.ToTable("lookup_type", "referencedata");
        }
    }
}
