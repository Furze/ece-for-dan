namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7AdvanceMonth
    {
        public int Id { get; set; }

        public int MonthNumber { get; set; }

        public int Year { get; set; }

        public int? AllDay { get; set; }

        public int? Sessional { get; set; }

        public int? ParentLed { get; set; }

        public int Rs7RevisionId { get; set; }

        public virtual Rs7Revision Rs7Revision { get; set; } = null!;
        
        internal Rs7AdvanceMonth Clone()
        {
            return new Rs7AdvanceMonth
            {
                MonthNumber = MonthNumber,
                Year = Year,
                AllDay = AllDay,
                Sessional = Sessional,
                ParentLed = ParentLed,
            };
        }
    }
}