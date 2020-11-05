using AutoMapper;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class DeclarationModelMapper : Profile
    {
        public DeclarationModelMapper()
        {
            CreateMap<Declaration, DeclarationModel>();
        }
    }
}