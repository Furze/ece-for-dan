using AutoMapper;
using MoE.ECE.Domain.Model.Rs7;
using Moe.ECE.Events.Integration.ELI;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementDayModelMapping : Profile
    {
        public Rs7EntitlementDayModelMapping()
        {
            CreateMap<Rs7EntitlementDay, Rs7EntitlementDayModel>();

            CreateMap<Rs7ReceivedEntitlementDay, Rs7EntitlementDayModel>();
        }
    }
}