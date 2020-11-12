using AutoMapper;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class DeclarationMapper : Profile
    {
        public DeclarationMapper()
        {
            CreateMap<DeclarationModel, Declaration>();
        }
    }
}