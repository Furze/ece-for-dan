using System.Collections.Generic;
using System.ComponentModel;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class SubmitRs7ForApproval : ICommand, IRs7AdvanceMonths, IRs7EntitlementMonths, IRs7Attestation
    {
        public IEnumerable<Rs7EntitlementMonthModel>? EntitlementMonths { get; set; } = new HashSet<Rs7EntitlementMonthModel>();
        public IEnumerable<Rs7AdvanceMonthModel>? AdvanceMonths { get; set; } = new HashSet<Rs7AdvanceMonthModel>();
        
        [ReadOnly(true)]
        public int Id { get; set; }
        public bool? IsAttested { get; set; }
    }
}