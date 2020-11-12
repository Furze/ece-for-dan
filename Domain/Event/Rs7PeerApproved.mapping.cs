using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Event
{
    public class Rs7PeerApprovedMapping : Profile
    {
        public Rs7PeerApprovedMapping()
        {
            CreateMap<Rs7, Rs7PeerApproved>()
                .Map(d => d.RevisionId, s => s.CurrentRevision.Id)
                .Map(d => d.Source, s => s.CurrentRevision.Source)
                .Map(d => d.RevisionNumber, s => s.CurrentRevision.RevisionNumber)
                .Map(d => d.RevisionDate, s => s.CurrentRevision.RevisionDate)
                .Map(d => d.AdvanceMonths, s => s.CurrentRevision.AdvanceMonths)
                .Map(d => d.EntitlementMonths, s => s.CurrentRevision.EntitlementMonths)
                .Map(d => d.IsAttested, s => s.CurrentRevision.IsAttested)
                .Map(d => d.Declaration, s => s.CurrentRevision.Declaration)
                ;
        }
    }
}