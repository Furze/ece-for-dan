using System;
using System.Collections.Generic;
using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class ECEServiceModelMapping : Profile
    {
        public ECEServiceModelMapping()
        {
            CreateMap<DateTimeOffset?, DateTimeOffset?>().ConvertUsing(s =>
                s != null && s.Value.Year == 1858 && s.Value.Month == 11 && s.Value.Day == 17 ? (DateTimeOffset?) null : s
            );

            CreateMap<ICollection<EceOperatingSession>, List<DailySessionModel>>()
                .ConvertUsing<OperatingSessionsConverter>();

            CreateMap<EceService, ECEServiceModel>()
                .Map(dest => dest.OrganisationId, src => src.RefOrganisationId)
                .Map(dest => dest.ServiceName, src => src.OrganisationName)
                .Map(dest => dest.ServiceProviderNumber, src => src.EceServiceProviderNumber)
                .Map(dest => dest.ServiceTypeId, src => src.OrganisationTypeId)
                .Map(dest => dest.ServiceTypeDescription, src => src.OrganisationTypeDescription)
                .Map(dest => dest.StatusDate, src => src.EceServiceStatusDate)
                .Map(dest => dest.StatusReasonId, src => src.EceServiceStatusReasonId)
                .Map(dest => dest.StatusReasonDescription, src => src.EceServiceStatusReasonDescription)
                .Map(dest => dest.PrimaryEmailAddress, src => src.Email)
                .Map(dest => dest.OtherEmailAddress, src => src.OtherEmail)
                .Map(dest => dest.AddressLine1, src => src.LocationAddressLine1)
                .Map(dest => dest.AddressLine2, src => src.LocationAddressLine2)
                .Map(dest => dest.AddressLine3, src => src.LocationAddressLine3)
                .Map(dest => dest.AddressLine4, src => src.LocationAddressLine4)
                .Map(d => d.DailySessions, s => s.OperatingSessions)
                .Ignore(dest => dest.CreatableRs7FundingPeriods);
        }
    }
}