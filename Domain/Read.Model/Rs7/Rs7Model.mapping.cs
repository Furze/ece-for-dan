using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7ModelMapping : Profile
    {
        public Rs7ModelMapping()
        {
            CreateMap<Domain.Model.Rs7.Rs7, Rs7Model>()
                .Map(dest => dest.RevisionNumber, src => src.CurrentRevision.RevisionNumber)
                .Map(dest => dest.RevisionDate, src => src.CurrentRevision.RevisionDate)
                .Map(dest => dest.AdvanceMonths, src => src.CurrentRevision.AdvanceMonths)
                .Map(dest => dest.EntitlementMonths, src => src.CurrentRevision.EntitlementMonths)
                .Map(dest => dest.IsAttested, src => src.CurrentRevision.IsAttested)
                .Map(dest => dest.Declaration, src => src.CurrentRevision.Declaration)
                .Map(dest => dest.IsZeroReturn, src => src.CurrentRevision.IsZeroReturn);

            CreateMap<Rs7Revision, Rs7Model>()
                .Ignore(d => d.BusinessEntityId)
                .Ignore(d => d.OrganisationId)
                .Ignore(d => d.FundingPeriod)
                .Ignore(d => d.FundingYear)
                .Ignore(d => d.FundingPeriodYear)
                .Ignore(d => d.RollStatus)
                .Ignore(d => d.ReceivedDate)
                .Ignore(d => d.RequestId);
        }
    }
}