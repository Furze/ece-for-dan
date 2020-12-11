using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceProviderDateRangedParameterConfiguration : EntityConfigurationBase<EceServiceProviderDateRangedParameter>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceServiceProviderDateRangedParameter> builder)
        {
            builder.HasKey(entity => new { entity.HistoryId, entity.AttributeSource });

            builder.HasOne(entity => entity.EceServiceProvider)
                .WithMany(entity => entity.EceServiceProviderDateRangedParameters)
                .HasForeignKey(entity => entity.RefOrganisationId);

            builder.ToTable("ece_service_provider_date_ranged_parameter", "referencedata");
        }
    }
}
