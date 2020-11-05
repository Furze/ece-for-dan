using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceDateRangedParameterConfiguration : EntityConfigurationBase<EceServiceDateRangedParameter>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceServiceDateRangedParameter> builder)
        {
            builder.HasKey(entity => new { entity.HistoryId, entity.AttributeSource });

            builder.HasIndex(entity => entity.RefOrganisationId);

            builder.ToTable("ece_service_date_ranged_parameter", "referencedata");
        }
    }
}
