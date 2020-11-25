using System.Collections.Generic;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class DailySessionModel
    {
        public int? DayOfWeek { get; set; }
        public string? Day { get; set; }
        public int? FundedHours { get; set; }
        public int? OperatingHours { get; set; }
        public List<OperatingSessionModel> OperatingTimes { get; set; } = new List<OperatingSessionModel>();
    }
}