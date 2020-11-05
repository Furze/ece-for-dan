using System.Collections.Generic;

namespace MoE.ECE.Domain.Services.Opa
{
    public class OpaRequest<T>
    {
        public IEnumerable<string> Outcomes { get; set; } = new List<string>();

        public IEnumerable<T> Cases { get; set; } = new List<T>();
    }
}
