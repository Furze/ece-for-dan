namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7AdvanceMonth
    {
        public int MonthNumber { get; set; }

        public int Year { get; set; }

        public int? AllDay { get; set; }

        public int? Sessional { get; set; }

        public int? ParentLed { get; set; }

        internal Rs7AdvanceMonth Clone() =>
            new Rs7AdvanceMonth
            {
                MonthNumber = MonthNumber,
                Year = Year,
                AllDay = AllDay,
                Sessional = Sessional,
                ParentLed = ParentLed
            };
    }
}