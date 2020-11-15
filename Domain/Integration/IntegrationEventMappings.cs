using System;
using AutoMapper;
using Events.Integration.Protobuf.Roll;
using Google.Protobuf.WellKnownTypes;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.Rs7;
using DeclarationModel = MoE.ECE.Domain.Read.Model.Rs7.DeclarationModel;
using IntegrationEvents = Events.Integration.Protobuf;
using Rs7Updated = MoE.ECE.Domain.Event.Rs7Updated;

namespace MoE.ECE.Domain.Integration
{
    public class IntegrationEventMappings : Profile
    {
        public IntegrationEventMappings()
        {
            CreateMap<string?, string>().ConvertUsing(source => source ?? string.Empty);
            CreateMap<Guid, ProtoBuf.Bcl.Guid>().ConvertUsing(guid => new ProtoBuf.Bcl.Guid(guid));
            CreateMap<DateTimeOffset?, Timestamp>()
                .ConvertUsing(offset => Timestamp.FromDateTimeOffset(offset.GetValueOrDefault()));
            CreateMap<DateTimeOffset, Timestamp>().ConvertUsing(offset => Timestamp.FromDateTimeOffset(offset));

            CreateMap<Rs7Updated, Events.Integration.Protobuf.Roll.Rs7Updated>();
            CreateMap<Rs7DeclarationUpdated, Events.Integration.Protobuf.Roll.Rs7Updated>();
            CreateMap<Rs7CreatedFromExternal, Events.Integration.Protobuf.Roll.Rs7Updated>();
            CreateMap<Rs7Approved, Events.Integration.Protobuf.Roll.Rs7Updated>();
            CreateMap<Rs7ZeroReturnCreated, Events.Integration.Protobuf.Roll.Rs7Updated>();

            CreateMap<Rs7EntitlementMonthUpdated, Events.Integration.Protobuf.Roll.Rs7Updated>();

            CreateMap<DeclarationModel, Events.Integration.Protobuf.Roll.DeclarationModel>()
                .Ignore(d => d.Id);

            CreateMap<Rs7AdvanceMonthModel, Rs7AdvanceMonth>()
                // TODO: Update PROTO files to remove these fields..
                .Ignore(d => d.Id)
                .Ignore(d => d.Days);

            CreateMap<Rs7EntitlementMonthModel, Rs7EntitlementMonth>()
                .Ignore(d => d.Id);

            CreateMap<Rs7EntitlementDayModel, Rs7EntitlementDay>()
                .Map(d => d.Over1, s => s.TwoAndOver);
        }
    }
}