namespace MoE.ECE.Domain.Exceptions
{
    public static class ErrorCode
    {
        public const string EceServiceNotFound = "1";
        public const string AdvancedDaysOfOperationMissing = "2";
        public const string InvalidMonthNumber = "3";
        public const string InvalidMonthForFundingPeriod = "4";
        public const string InvalidRs3FundingEffectiveDate = "5";
        public const string InvalidUpdateRs7StatusNew = "6";
        public const string InvalidRollStatusTransition = "7";
        public const string InvalidRollStatusForDiscard = "8";
        public const string DuplicateResourceFundingPeriod = "9";      
        public const string EceServiceIneligibleBecauseStatusClosed = "10";
        public const string EceServiceIneligibleBecauseLicenceStatus = "11";
        public const string InvalidRollStatusForSubmitRs7ForApproval = "12";
        public const string InvalidRollStatusForUpdate = "13";
        public const string InvalidRollStatusForPeerRejectingRs7 = "InvalidRollStatusForPeerRejectingRs7";
        
        // Fluent Validation Error Codes
        public const string InvalidDayCount = "InvalidDayCount";
        public const string LessThanOrEqualValidator = "LessThanOrEqualValidator";
        public const string GreaterThanValidator = "GreaterThanValidator";
        public const string NotEmptyValidator = "NotEmptyValidator";
        public const string InclusiveBetweenValidator = "InclusiveBetweenValidator";
    }
}