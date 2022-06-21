namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.ReviewApprove
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

    public class ReviewApproveStateConst
    {
        public const string Key = "ReviewApproveState";
    }

    public class ReviewApproveStateShortCase
    {
        public static string Recalled = "R";

        public static string Rejected = "R";

        public static string Approved = "A";
    }

    public enum OverallReviewApproveState
    {
        NeverApproved = 0,  //(blank)
        Approved,           //Approved
        OnceApproved,       //Approved*
        Unknown = 10000,
    }

    public enum ReviewApproveVersionState
    {
        Approved = 0,
        Rejected,
        Recalled,
        NotApprovedOrRejectedOrRecalled,
        Unknown = 10000,
    }

    public static class ReviewApproveStateConverter
    {
        public static ReviewApproveVersionState GetVersionState(ReviewApproveState state)
        {
            ReviewApproveVersionState retValue = ReviewApproveVersionState.Unknown;
            switch (state)
            {
                case ReviewApproveState.Recalled:
                    retValue =  ReviewApproveVersionState.Recalled;
                    break;
                case ReviewApproveState.Rejected:
                    retValue =  ReviewApproveVersionState.Rejected;
                    break;
                case ReviewApproveState.Approved:
                    retValue =  ReviewApproveVersionState.Approved;
                    break;
                default:
                    retValue =  ReviewApproveVersionState.Unknown;
                    break;
            }
            return retValue;
        }

        public static string GetVersionStateString(ReviewApproveState state)
        {
            string retValue ;
            switch (state)
            {
                case ReviewApproveState.Recalled:
                    retValue = "Recalled";
                    break;
                case ReviewApproveState.Rejected:
                    retValue = "Rejected";
                    break;
                case ReviewApproveState.Approved:
                    retValue = "Approved";
                    break;
                default:
                    retValue =string.Empty;
                    break;
            }
            return retValue;
        }
        public static string GetReviewApproveStateShortCase(ReviewApproveVersionState versionState)
        {
            switch (versionState)
            {
                case ReviewApproveVersionState.Approved:
                    return ReviewApproveStateShortCase.Approved;
                case ReviewApproveVersionState.Recalled:
                    return ReviewApproveStateShortCase.Recalled;
                case ReviewApproveVersionState.Rejected:
                    return ReviewApproveStateShortCase.Rejected;
            }
            return string.Empty;
        }

        public static string GetOverallStateString(OverallReviewApproveState overallState)
        {
            string retValue;
            switch (overallState)
            {
                case OverallReviewApproveState.Approved:
                    retValue = "Approved";
                    break;
                case OverallReviewApproveState.OnceApproved:
                    retValue = "Approved*";
                    break;
                default:
                    retValue = string.Empty;
                    break;
            }
            return retValue;
        }

    }
}
