using System;
using System.Text.Json.Serialization;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Read.Model
{
    public class RollModel
    {
        public int Id { get; set; }

        public Guid? BusinessEntityId { get; set; }

        public int? OrganisationId { get; set; }

        public string? RollType { get; set; }

        public DateTimeOffset? Received { get; set; }

        public RollStatus? Status { get; set; }

        public string? AssignedTo { get; set; }

        [JsonConverter(typeof(JsonNoEnumConverter<FundingPeriodMonth>))]
        public FundingPeriodMonth FundingPeriodMonth { get; set; } = new FundingPeriodMonth();

        public int? FundingYear { get; set; }

        public int? FundingPeriodYear { get; set; }
    }
}