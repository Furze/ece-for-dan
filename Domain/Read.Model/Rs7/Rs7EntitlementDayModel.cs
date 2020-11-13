namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementDayModel
    {
        public int DayNumber { get; set; }

        public int? Under2 { get; set; }

        public int? TwoAndOver { get; set; }

        public int? Hours20 { get; set; }

        public int? Plus10 { get; set; }

        public int? Certificated { get; set; }

        public int? NonCertificated { get; set; }
    }
}