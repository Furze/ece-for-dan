using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class
        EceLicencingDetailDateRangedParameterConfiguration : EntityConfigurationBase<
            EceLicencingDetailDateRangedParameter>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceLicencingDetailDateRangedParameter> builder)
        {
            builder.HasKey(entity => entity.LicencingDetailHistoryId);

            builder.HasMany(entity => entity.EceOperatingSessionDateRangedParameters)
                .WithOne(entity => entity.EceLicencingDetailDateRangedParameter)
                .HasForeignKey(p => p.LicencingDetailHistoryId);

            builder.HasIndex(entity => entity.RefOrganisationId);

            builder.ToTable("ece_licencing_detail_date_ranged_parameter", "referencedata");
        }
    }
}