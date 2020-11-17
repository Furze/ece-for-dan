using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class SearchEceServiceModelMapper : Profile
    {
        public SearchEceServiceModelMapper()
        {
            CreateMap<EceService, SearchEceServiceModel>()
                .Map(d => d.OrganisationId, s => s.RefOrganisationId)
                .Map(d => d.ServiceName, s => s.OrganisationName)
                .Map(d => d.ServiceProviderNumber, s => s.EceServiceProviderNumber);
        }
    }
}