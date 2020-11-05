using Microsoft.EntityFrameworkCore;

namespace MoE.ECE.Domain.Infrastructure.EntityFramework
{
    public class ReferenceDataContext : DbContext
    {
        public ReferenceDataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
