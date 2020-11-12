using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Event
{
    public class Rs7CreatedMapping : Profile
    {
        public Rs7CreatedMapping()
        {
            CreateMap<Rs7, Rs7SkeletonCreated>()
                .Map(d => d.RevisionNumber, s => s.CurrentRevision.RevisionNumber)
                .Map(d => d.RevisionDate, s => s.CurrentRevision.RevisionDate)
                .Map(d => d.AdvanceMonths, s => s.CurrentRevision.AdvanceMonths)
                .Map(d => d.EntitlementMonths, s => s.CurrentRevision.EntitlementMonths)
                .Map(d => d.IsAttested, s => s.CurrentRevision.IsAttested)
                .Map(d => d.Declaration, s => s.CurrentRevision.Declaration)
                .Map(d => d.IsZeroReturn, s => s.CurrentRevision.IsZeroReturn)
                .Map(d => d.Source, s => s.CurrentRevision.Source);
        }
    }
}