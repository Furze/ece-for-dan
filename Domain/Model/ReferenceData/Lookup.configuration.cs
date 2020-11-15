using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class LookupConfiguration : EntityConfigurationBase<Lookup>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.ToTable("lookup", "referencedata");
        }
    }
}