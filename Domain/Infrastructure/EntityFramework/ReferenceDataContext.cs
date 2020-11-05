using Microsoft.EntityFrameworkCore;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Infrastructure.EntityFramework
{
    public class ReferenceDataContext : DbContext
    {
        public ReferenceDataContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<EceService> EceServices { get; set; } = null!;
    }
}
