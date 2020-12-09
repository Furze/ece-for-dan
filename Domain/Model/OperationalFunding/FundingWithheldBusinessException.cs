namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class FundingWithheldBusinessException : BusinessException
    {
        public const string FundingWithheldKey = "FundingWithheld";

        public FundingWithheldBusinessException()
        {
            Key = FundingWithheldKey;
            Description = "This service is not funded";
        }
    }
    
    public class ServiceMonthlyFundingBusinessException : BusinessException
    {
        public const string MonthlyFundingKey = "MonthlyFunding";

        public ServiceMonthlyFundingBusinessException()
        {
            Key = MonthlyFundingKey;
            Description = "This service is funded monthly";
        }
    }
}