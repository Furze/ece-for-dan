namespace Moe.ECE.Events.Integration.ELI
{
    public class Rs7ReceivedAdvanceMonth
    {
        public int MonthNumber { get; set; }

        public int FundingPeriodYear { get; set; }

        public int? AllDay { get; set; }

        public int? Sessional { get; set; }

        public int? ParentLed { get; set; }
    }
}