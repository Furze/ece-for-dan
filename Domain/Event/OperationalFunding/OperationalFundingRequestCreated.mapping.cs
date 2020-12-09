using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Event.OperationalFunding
{
    public class OperationalFundingRequestCreatedMapping : Profile
    {
        public OperationalFundingRequestCreatedMapping()
        {
            CreateMap<OperationalFundingRequest, OperationalFundingRequestCreated>()
                .Ignore(d => d.Equity)
                .Ignore(d => d.MatchingAdvanceMonths);
        }
    }
}