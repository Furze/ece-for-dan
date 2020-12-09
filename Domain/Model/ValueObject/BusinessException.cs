namespace MoE.ECE.Domain.Model.ValueObject
{
    public class BusinessException
    {
        public BusinessExceptionClassification Classification { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}