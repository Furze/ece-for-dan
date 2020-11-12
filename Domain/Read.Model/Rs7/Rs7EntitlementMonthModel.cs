using System.ComponentModel;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementMonthModel
    {
        public int? MonthNumber { get; set; }

        [ReadOnly(true)] public int Year { get; set; }

        public Rs7EntitlementDayModel[]? Days { get; set; }
    }
}