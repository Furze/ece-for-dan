using MoE.ECE.Domain.Read.Model.OperationalFunding;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Event.OperationalFunding
{
    public class OperationalFundingRequestCreated : OperationalFundingRequestModel, IDomainEvent
    {
    }
}