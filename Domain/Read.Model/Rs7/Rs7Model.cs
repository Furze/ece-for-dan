using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7Model : IRs7AdvanceMonths, IRs7EntitlementMonths, IRs7Attestation
    {
        [ReadOnly(true)]
        public int Id { get; set; }

        [ReadOnly(true)]
        public Guid? BusinessEntityId { get; set; }

        [ReadOnly(true)]
        public int? OrganisationId { get; set; }

        [ReadOnly(true)]
        [JsonConverter(typeof(JsonNoEnumConverter<FundingPeriodMonth>))]
        public FundingPeriodMonth FundingPeriod { get; set; }

        [ReadOnly(true)]
        public int FundingYear { get; set; }

        [ReadOnly(true)]
        public int FundingPeriodYear { get; set; }

        public bool? IsZeroReturn { get; set; }

        public RollStatus RollStatus { get; set; }

        public DateTimeOffset? ReceivedDate { get; set; }

        [ReadOnly(true)]
        public int RevisionNumber { get; set; }

        [ReadOnly(true)]
        public DateTimeOffset RevisionDate { get; set; }

        public IEnumerable<Rs7AdvanceMonthModel>? AdvanceMonths { get; set; } = new HashSet<Rs7AdvanceMonthModel>();

        public IEnumerable<Rs7EntitlementMonthModel>? EntitlementMonths { get; set; } = new HashSet<Rs7EntitlementMonthModel>();

        public bool? IsAttested { get; set; }

        public DeclarationModel? Declaration { get; set; }
        
        public string? RequestId { get; set; }
    }
}