using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Request
{
    public class AdvanceMonth
    {
        [JsonProperty(PropertyName = "@id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "advMonthNumber")]
        public int MonthNumber { get; set; }

        [JsonProperty(PropertyName = "advMonthName")]
        public string? MonthName { get; set; }

        [JsonProperty(PropertyName = "advMonthAllDayWorkingDays")]
        public int? AllDayWorkingDays { get; set; }

        [JsonProperty(PropertyName = "advMonthSessionalWorkingDays")]
        public int? SessionalWorkingDays { get; set; }

        [JsonProperty(PropertyName = "advMonthParentLedWorkingDays")]
        public int? ParentLedWorkingDays { get; set; }
    }
}