namespace MoE.ECE.Domain.Model.ValueObject
{
    public enum RollStatus
    {
        /// <summary>
        /// Created in Skeleton State
        /// </summary>
        ExternalNew = 0,
        /// <summary>
        /// Created and Saved as Draft
        /// </summary>
        ExternalDraft = 1,
        /// <summary>
        /// Has been Approved externally and now is waiting for Internal Ministry Approval.
        /// </summary>
        InternalReadyForReview = 2,
        /// <summary>
        /// Has been Approved by the internal Ministry (Three Step) Approval Process
        /// </summary>
        InternalApproved = 3,
        /// <summary>
        /// Completed and awaiting Approval by external (Two Step) Approval Process
        /// </summary>
        ExternalSubmittedForApproval = 4,
        /// <summary>
        /// Returned from external (Two Step) Approval Process.
        /// </summary>
        ExternalReturnedForEdit = 5,
        /// <summary>
        /// Declined pure and simple..
        /// </summary>
        Declined = 6
    }
}
   