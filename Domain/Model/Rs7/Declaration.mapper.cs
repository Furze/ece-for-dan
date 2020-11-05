using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class DeclarationMapper : Profile
    {
        public DeclarationMapper()
        {
            CreateMap<DeclarationModel, Declaration>()
                .Ignore(declaration => declaration.Id)
                .Ignore(declaration => declaration.RowVersion)
                .Ignore(declaration => declaration.Rs7Revision)
                .Ignore(declaration => declaration.Rs7RevisionId);
        }
    }
}