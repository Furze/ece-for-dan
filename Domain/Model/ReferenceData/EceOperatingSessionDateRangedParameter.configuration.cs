using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceOperatingSessionDateRangedParameterConfiguration : EntityConfigurationBase<EceOperatingSessionDateRangedParameter>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceOperatingSessionDateRangedParameter> builder)
        {
            builder.HasKey(entity => new { entity.LicencingDetailHistoryId, entity.OperatingSessionId });

            builder.HasOne(entity => entity.EceLicencingDetailDateRangedParameter)
                .WithMany(entity => entity.EceOperatingSessionDateRangedParameters)
                .HasForeignKey(entity => entity.LicencingDetailHistoryId);

            builder.ToTable("ece_operating_session_date_ranged_parameter", "referencedata");
        }
    }
}
