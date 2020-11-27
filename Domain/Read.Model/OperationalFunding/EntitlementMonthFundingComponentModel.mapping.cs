using AutoMapper;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class EntitlementMonthFundingComponentModelMapping : Profile
    {
        public EntitlementMonthFundingComponentModelMapping()
        {
            CreateMap<EntitlementMonthFundingComponent, EntitlementMonthFundingComponentModel>();
        }
    }
}