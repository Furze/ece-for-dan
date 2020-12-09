using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class BusinessExceptionModel
    {
        public BusinessExceptionClassification Classification { get; set; }

        public string Key { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}