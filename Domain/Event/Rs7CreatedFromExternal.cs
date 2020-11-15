using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Event
{
    /// <summary>
    ///     Domain event to indicate that an Rs7 return was created, not via data submitted
    ///     directly to Eli cloud, but rather via an external SMS system.
    /// </summary>
    public class Rs7CreatedFromExternal : Rs7Model, IDomainEvent
    {
        public int RevisionId { get; set; }
        public string? Source { get; set; }
    }

    public class Rs7CreatedFromExternalMapping : Profile
    {
        public Rs7CreatedFromExternalMapping() =>
            CreateMap<Rs7, Rs7CreatedFromExternal>()
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