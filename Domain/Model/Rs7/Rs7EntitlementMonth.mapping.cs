using AutoMapper;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7EntitlementMonthMapping : Profile
    {
        public Rs7EntitlementMonthMapping()
        {
            CreateMap<Rs7EntitlementMonthModel, Rs7EntitlementMonth>();
        }
    }
}