using AutoMapper;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Infrastructure.Extensions;

namespace MoE.ECE.Domain.Command
{
    public class CreateOperationalFundingRequestMapper : Profile
    {
        public CreateOperationalFundingRequestMapper()
        {
            CreateMap<Rs7Updated, CreateOperationalFundingRequest>()
                .Map(d => d.Rs7Id, s => s.Id)
                .Map(d => d.FundingPeriodMonth, s => s.FundingPeriod);
        }
    }
}