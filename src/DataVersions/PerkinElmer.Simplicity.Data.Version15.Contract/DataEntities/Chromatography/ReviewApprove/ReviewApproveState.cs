namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove
{
    public enum ReviewApproveState
    {
        NeverSubmitted = 0,
        Recalled,
        Rejected,
        PendingReview,
        InReview,
        PendingApproval, // Reviewed is a transient state, after review passed, it is pending approval state.
        InApproval,
        Approved,

        SubItemReviewPassed = 100,
        SubItemReviewRejected,
        Unknown = 10000,
    }
}
