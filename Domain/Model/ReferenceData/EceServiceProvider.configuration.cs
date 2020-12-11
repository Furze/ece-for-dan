using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceProviderConfiguration : EntityConfigurationBase<EceServiceProvider>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceServiceProvider> builder)
        {
            builder.HasKey(entity => entity.RefOrganisationId);

            builder.HasIndex(entity => entity.OrganisationNumber);

            builder.ToTable("ece_service_provider", "referencedata");
        }
    }
}
