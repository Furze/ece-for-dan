using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Event.OperationalFunding;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.ECE.Events.Integration;
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

            CreateMap<Rs7Updated, IntegrationEvents.Ece.Rs7Updated>();
            CreateMap<Rs7DeclarationUpdated, IntegrationEvents.Ece.Rs7Updated>();
            CreateMap<FullRs7Created, IntegrationEvents.Ece.Rs7Updated>();
            CreateMap<Rs7Approved, IntegrationEvents.Ece.Rs7Updated>();
            CreateMap<Rs7ZeroReturnCreated, IntegrationEvents.Ece.Rs7Updated>();
            
            CreateMap<Rs7EntitlementMonthUpdated, IntegrationEvents.Ece.Rs7Updated>();

            CreateMap<DeclarationModel, IntegrationEvents.Ece.DeclarationModel>()
                .Ignore(d => d.Id);
            
            CreateMap<Rs7AdvanceMonthModel, IntegrationEvents.Ece.Rs7AdvanceMonth>()
                // TODO: Update PROTO files to remove these fields..
                .Ignore(d => d.Id)
                .Ignore(d => d.Days);
                            
            CreateMap<Rs7EntitlementMonthModel, IntegrationEvents.Ece.Rs7EntitlementMonth>()
                .Ignore(d => d.Id);
            
            CreateMap<Rs7EntitlementDayModel, IntegrationEvents.Ece.Rs7EntitlementDay>()
                .Map(d => d.Over1, s => s.TwoAndOver);

            CreateMap<OperationalFundingRequestCreated, IntegrationEvents.Ece.EntitlementCalculated>()
                .Map(d => d.BusinessEntityType, s => Constants.BusinessEntityTypes.Rs7)
                .Map(d => d.Exceptions, s => s.BusinessExceptions)
                .Ignore(d => d.FundingPeriodYear);

            CreateMap<BusinessExceptionModel, IntegrationEvents.Ece.EntitlementException>();
        }
    }
}