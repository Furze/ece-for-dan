using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceConfiguration : EntityConfigurationBase<EceService>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceService> builder)
        {
            builder.HasKey(entity => entity.RefOrganisationId);

            builder.Property(entity => entity.IsolationIndex)
                .HasColumnType("decimal(12, 2)");

            builder.HasOne(entity => entity.EceServiceProvider)
                .WithMany(entity => entity.EceServices)
                .HasForeignKey(entity => entity.EceServiceProviderId);

            builder.HasIndex(entity => entity.OrganisationNumber);

            builder.ToTable("ece_service", "referencedata");
        }
    }
}
