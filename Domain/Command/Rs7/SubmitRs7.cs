using System.Collections.Generic;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class SubmitRs7 : ICommand
    {
        public IEnumerable<Rs7EntitlementMonthModel>? EntitlementMonths { get; set; } = new HashSet<Rs7EntitlementMonthModel>();

        public bool? IsAttested { get; set; }
    }
}