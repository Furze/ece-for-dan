using AutoMapper;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class BusinessExceptionModelMapping : Profile
    {
        public BusinessExceptionModelMapping()
        {
            CreateMap<BusinessException, BusinessExceptionModel>();
        }
    }
}