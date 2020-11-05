namespace MoE.ECE.Domain.Exceptions
{
    public static class ErrorCode
    {
        public static string RequiredField => AppendWorkflowPrefix("RequiredField");

        public static string InvalidEnumValue => AppendWorkflowPrefix("InvalidWorkActionType");

        public static string PreExistingProvisionalCycle => AppendProvisionalCyclePrefix("CycleExists");
        public static string InvalidStateProvisionalCycle => AppendProvisionalCyclePrefix("CycleStateChangeInvalid");

        private static string AppendWorkflowPrefix(string code)
        {
            return AppendPrefix("Workflow", code);
        }
        private static string AppendProvisionalCyclePrefix(string code)
        {
            return AppendPrefix("ProvisionalCycle", code);
        }
        private static string AppendPrefix(string subject, string code)
        {
            return $"{subject}::{code}";
        }
    }
}