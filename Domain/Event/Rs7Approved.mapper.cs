using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Event
{
    public class Rs7ApprovedMapper : Profile
    {
        public Rs7ApprovedMapper() =>
            CreateMap<Rs7, Rs7Approved>()
                .Map(dest => dest.RevisionId, src => src.CurrentRevision.Id)
                .Map(d => d.RevisionNumber, s => s.CurrentRevision.RevisionNumber)
                .Map(d => d.RevisionDate, s => s.CurrentRevision.RevisionDate)
                .Map(d => d.AdvanceMonths, s => s.CurrentRevision.AdvanceMonths)
                .Map(d => d.EntitlementMonths, s => s.CurrentRevision.EntitlementMonths)
                .Map(d => d.IsAttested, s => s.CurrentRevision.IsAttested)
                .Map(d => d.Declaration, s => s.CurrentRevision.Declaration);
    }
}