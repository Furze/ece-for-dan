using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command
{
    public class CreateOperationalFundingRequest : ICommand
    {
        public Guid BusinessEntityId { get; set; }

        public int OrganisationId { get; set; }
        
        public int Rs7Id { get; set; }

        public FundingPeriodMonth? FundingPeriodMonth { get; set; }

        public int FundingYear { get; set; }

        public int RevisionNumber { get; set; }

        public IEnumerable<Rs7EntitlementMonth>? EntitlementMonths { get; set; }

        public IEnumerable<Rs7AdvanceMonth>? AdvanceMonths { get; set; }

        public bool IsAttested { get; set; }
        
        public string? RequestId { get; set; }
    }
}