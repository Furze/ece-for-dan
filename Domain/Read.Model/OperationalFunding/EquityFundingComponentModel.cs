namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    /// <summary>
    /// TODO - Need to check remaining fields....
    /// </summary>
    public class EquityFundingComponentModel
    {
        public int FundedChildHours { get; set; }

        public string? EquityIndex { get; set; }

        public decimal LowSocioeconomicAmount { get; set; }

        public decimal LowSocioeconomicRate { get; set; }
    }
}