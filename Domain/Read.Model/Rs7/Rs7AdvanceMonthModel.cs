using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7AdvanceMonthModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int MonthNumber { get; set; }

        [ReadOnly(true)]
        public int Year { get; set; }

        public int? AllDay { get; set; }

        public int? Sessional { get; set; }

        public int? ParentLed { get; set; }
    }
}