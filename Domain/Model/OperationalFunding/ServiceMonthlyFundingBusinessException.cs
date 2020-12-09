using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class ServiceMonthlyFundingBusinessException : BusinessException
    {
        public const string MonthlyFundingKey = "MonthlyFunding";

        public ServiceMonthlyFundingBusinessException()
        {
            Key = MonthlyFundingKey;
            Description = "Service is on monthly funding";
            Classification = BusinessExceptionClassification.Other;
        }
    }
}