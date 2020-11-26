using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceLicencingDetailDateRangedParameterConfiguration : EntityConfigurationBase<EceLicencingDetailDateRangedParameter>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceLicencingDetailDateRangedParameter> builder)
        {
            builder.HasKey(entity => entity.LicencingDetailHistoryId);

            builder.HasOne(entity => entity.EceService)
                .WithMany(entity => entity.EceLicencingDetailDateRangedParameters)
                .HasForeignKey(entity => entity.RefOrganisationId);

            builder.ToTable("ece_licencing_detail_date_ranged_parameter", "referencedata");
        }
    }
}
