using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementMonthModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int? MonthNumber { get; set; }

        [ReadOnly(true)]
        public int Year { get; set; }

        public ICollection<Rs7EntitlementDayModel>? Days { get; set; }
    }
}