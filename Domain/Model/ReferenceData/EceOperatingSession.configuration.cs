using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceOperatingSessionConfiguration : EntityConfigurationBase<EceOperatingSession>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EceOperatingSession> builder)
        {
            builder.HasKey(entity => entity.OperatingSessionId);

            builder.ToTable("ece_operating_session", "referencedata");
        }
    }
}