using System.Collections.Generic;

namespace MoE.ECE.Domain.Infrastructure.Services.Opa
{
    public class OpaResponse<T>
    {
        public IEnumerable<T> Cases { get; set; } = new List<T>();
        public string OriginalResponse { get; set; } = string.Empty;
    }
}
