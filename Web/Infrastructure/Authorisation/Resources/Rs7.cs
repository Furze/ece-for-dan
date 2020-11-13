namespace MoE.ECE.Web.Infrastructure.Authorisation.Resources
{
    public static class Rs7
    {
        private const string NewRequests = ResourceName + "-new-requests";
        private const string Rs7DraftChanges = ResourceName + "-draft-changes";
        private const string Rs7Submissions = ResourceName + "-submissions";
        private const string Rs7SubmissionsForApproval = ResourceName + "-submissions-for-approval";
        private const string Rs7EntitlementMonth = ResourceName + "-entitlementMonth";
        private const string Rs7Declaration = ResourceName + "-declaration";
        private const string Rs7ZeroReturns = ResourceName + "-zero-returns";

        public const string ResourceName = "rs7";
        public const string List = ResourceName + ActionNames.List;
        public const string View = ResourceName + ActionNames.View;
        public const string Delete = ResourceName + ActionNames.Delete;

        public const string NewRequestsCreate = NewRequests + ActionNames.Create;
        public const string DraftChangesCreate = Rs7DraftChanges + ActionNames.Create;
        public const string SubmissionsForApprovalCreate = Rs7SubmissionsForApproval + ActionNames.Create;
        public const string SubmissionsCreate = Rs7Submissions + ActionNames.Create;
        public const string EntitlementMonthUpdate = Rs7EntitlementMonth + ActionNames.Update;
        public const string DeclarationUpdate = Rs7Declaration + ActionNames.Update;
        public const string ZeroReturnsCreate = Rs7ZeroReturns + ActionNames.Create;
    }
}