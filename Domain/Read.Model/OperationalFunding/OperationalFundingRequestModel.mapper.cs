using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class OperationalFundingRequestModelMapper : Profile
    {
        public OperationalFundingRequestModelMapper()
        {
            CreateMap<OperationalFundingRequest, OperationalFundingRequestModel>()
                .Ignore(d => d.MatchingAdvanceMonths)
                .Ignore(d => d.Equity);
        }
    }
}