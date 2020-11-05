using MoE.ECE.Domain.Model.ValueObject;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateRs7 : IBeginSagaCommand
    {
        public int? OrganisationId { get; set; }

        //TODO Should be renamed to FundingPeriodMonth
        public FundingPeriodMonth FundingPeriod { get; set; }

        public int FundingPeriodYear { get; set; }
    }
}