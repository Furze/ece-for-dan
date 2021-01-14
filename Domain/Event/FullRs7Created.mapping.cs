using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Event
{
    public class FullRs7CreatedMapping : Profile
    {
        public FullRs7CreatedMapping()
        {
            CreateMap<Rs7, FullRs7Created>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.Source, s => s.CurrentRevision.Source)
                .Map(d => d.RevisionNumber, s => s.CurrentRevision.RevisionNumber)
                .Map(d => d.EntitlementMonths, s => s.CurrentRevision.EntitlementMonths)
                .Map(d => d.AdvanceMonths, s => s.CurrentRevision.AdvanceMonths)
                .Map(d => d.RevisionDate, s => s.CurrentRevision.RevisionDate)
                .Map(d => d.IsAttested, s => s.CurrentRevision.IsAttested)
                .Map(d => d.Declaration, s => s.CurrentRevision.Declaration)
                .Map(d => d.RevisionId, s => s.CurrentRevision.Id);
        }
    }
}