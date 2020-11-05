using Marten;

namespace MoE.ECE.Domain.Infrastructure
{
    public class MartenSettings
    {
        public string SchemaName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;

        public AutoCreate AutoCreate { get; set; } = AutoCreate.None;
    }
}