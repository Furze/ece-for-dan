using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.Rs7;
using IntegrationEvents = Events.Integration.Protobuf;

namespace MoE.ECE.Domain.Integration
{
    public class IntegrationEventMappings : Profile
    {
        public IntegrationEventMappings()
        {
            CreateMap<string?, string>().ConvertUsing(source => source ?? string.Empty);
            CreateMap<Guid, ProtoBuf.Bcl.Guid>().ConvertUsing(guid => new ProtoBuf.Bcl.Guid(guid));
            CreateMap<DateTimeOffset?, Timestamp>().ConvertUsing(offset => Timestamp.FromDateTimeOffset(offset.GetValueOrDefault()));
            CreateMap<DateTimeOffset, Timestamp>().ConvertUsing(offset => Timestamp.FromDateTimeOffset(offset));

            CreateMap<Rs7Updated, IntegrationEvents.Roll.Rs7Updated>();
            CreateMap<Rs7DeclarationUpdated, IntegrationEvents.Roll.Rs7Updated>();
            CreateMap<Rs7CreatedFromExternal, IntegrationEvents.Roll.Rs7Updated>();
            CreateMap<Rs7Approved, IntegrationEvents.Roll.Rs7Updated>();
            CreateMap<Rs7ZeroReturnCreated, IntegrationEvents.Roll.Rs7Updated>();
            
            CreateMap<Rs7EntitlementMonthUpdated, IntegrationEvents.Roll.Rs7Updated>();

            CreateMap<DeclarationModel, IntegrationEvents.Roll.DeclarationModel>();
            
            CreateMap<Rs7AdvanceMonthModel, IntegrationEvents.Roll.Rs7AdvanceMonth>()
                .Ignore(d => d.Days);
                            
            CreateMap<Rs7EntitlementMonthModel, IntegrationEvents.Roll.Rs7EntitlementMonth>();
            CreateMap<Rs7EntitlementDayModel, IntegrationEvents.Roll.Rs7EntitlementDay>()
                .Map(d => d.Over1, s => s.TwoAndOver);
        }
    }
}