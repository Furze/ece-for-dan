using System.ComponentModel;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7EntitlementDay
    {
        [ReadOnly(true)]
        public int? Id { get; set; }

        public int? DayNumber { get; set; }

        public int? Under2 { get; set; }

        public int? TwoAndOver { get; set; }

        public int? Hours20 { get; set; }

        public int? Plus10 { get; set; }

        public int? Certificated { get; set; }

        public int? NonCertificated { get; set; }

        public int Rs7EntitlementMonthId { get; set; }

        public virtual Rs7EntitlementMonth Rs7EntitlementMonth { get; set; } = null!;
        
        internal Rs7EntitlementDay Clone()
        {
            return new Rs7EntitlementDay
            {
                DayNumber = DayNumber,
                Under2 = Under2,
                TwoAndOver = TwoAndOver,
                Hours20 = Hours20,
                Plus10 = Plus10,
                Certificated = Certificated,
                NonCertificated = NonCertificated
            };
        }
    }
}