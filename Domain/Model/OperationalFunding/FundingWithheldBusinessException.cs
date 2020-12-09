using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class FundingWithheldBusinessException : BusinessException
    {
        public const string FundingWithheldKey = "FundingWithheld";

        public FundingWithheldBusinessException()
        {
            Key = FundingWithheldKey;
            Description = "Funding Withheld";
            Classification = BusinessExceptionClassification.High;
        }
    }
}