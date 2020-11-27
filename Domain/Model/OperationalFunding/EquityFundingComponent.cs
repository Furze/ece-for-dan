namespace MoE.ECE.Domain.Model.OperationalFunding
{
    /// <summary>
    /// TODO - Need to check remaining fields....
    /// </summary>
    public class EquityFundingComponent : DomainEntity
    {
        public int FundedChildHours { get; set; }

        public string? EquityIndex { get; set; }

        public decimal LowSocioeconomicAmount { get; set; }

        public decimal LowSocioeconomicRate { get; set; }
    }
}