using System.ComponentModel;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7AdvanceMonthModel
    {
        public int MonthNumber { get; set; }

        [ReadOnly(true)] public int Year { get; set; }

        public int? AllDay { get; set; }

        public int? Sessional { get; set; }

        public int? ParentLed { get; set; }
    }
}