using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Infrastructure.EntityFramework
{
    public class ReferenceDataContext : DbContext
    {
        public ReferenceDataContext(DbContextOptions<ReferenceDataContext> options, IEnumerable<IEntityConfiguration> configurations)
            : base(options)
        {
            _configurations = configurations;
        }

        private readonly IEnumerable<IEntityConfiguration> _configurations;

        public DbSet<EceService> EceServices { get; set; } = null!;

        public DbSet<EceServiceDateRangedParameter> EceServiceDateRangedParameters { get; set; } = null!;

        public DbSet<EceOperatingSession> EceOperatingSessions { get; set; } = null!;

        public DbSet<EceOperatingSessionDateRangedParameter> EceOperatingSessionDateRangedParameters { get; set; } = null!;

        public DbSet<EceLicencingDetailDateRangedParameter> EceLicencingDetailDateRangedParameters { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _configurations.ToList().ForEach(c => c.ApplyConfiguration(modelBuilder));
        }
    }
}
