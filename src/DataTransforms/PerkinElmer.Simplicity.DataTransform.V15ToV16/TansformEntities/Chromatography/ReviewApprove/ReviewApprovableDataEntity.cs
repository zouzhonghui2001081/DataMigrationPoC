using ReviewApprovableDataEntity15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove.ReviewApprovableDataEntity;
using ReviewApprovableDataEntity16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove.ReviewApprovableDataEntity;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ReviewApprove
{
    public class ReviewApprovableDataEntity
    {
        public static ReviewApprovableDataEntity16 Transform(
            ReviewApprovableDataEntity15 reviewApprovableDataEntity)
        {
            var reviewApprovableDataEntity16 = new ReviewApprovableDataEntity16
            {
                Id = reviewApprovableDataEntity.Id,
                ProjectName = reviewApprovableDataEntity.ProjectName,
                ProjectId = reviewApprovableDataEntity.ProjectId,
                EntityId = reviewApprovableDataEntity.EntityId,
                InReviewBy = reviewApprovableDataEntity.InReviewBy,
                InApproveBy = reviewApprovableDataEntity.InApproveBy,
                ReviewedBy = reviewApprovableDataEntity.ReviewedBy,
                ApprovedBy = reviewApprovableDataEntity.ApprovedBy,
                RejectedBy = reviewApprovableDataEntity.RejectedBy,
                RecalledBy = reviewApprovableDataEntity.RecalledBy,
                PostponedBy = reviewApprovableDataEntity.PostponedBy,
                SubmittedBy = reviewApprovableDataEntity.SubmittedBy,
                LastModifiedBy = reviewApprovableDataEntity.LastModifiedBy,
                InReviewByUserId = reviewApprovableDataEntity.InReviewByUserId,
                InApproveByUserId = reviewApprovableDataEntity.InApproveByUserId,
                ReviewedByUserId = reviewApprovableDataEntity.ReviewedByUserId,
                ApprovedByUserId = reviewApprovableDataEntity.ApprovedByUserId,
                RejectedByUserId = reviewApprovableDataEntity.RejectedByUserId,
                RecalledByUserId = reviewApprovableDataEntity.RecalledByUserId,
                PostponedByUserId = reviewApprovableDataEntity.PostponedByUserId,
                SubmittedByUserId = reviewApprovableDataEntity.SubmittedByUserId,
                LastModifiedByUserId = reviewApprovableDataEntity.LastModifiedByUserId,
                SubmitTimestamp = reviewApprovableDataEntity.SubmitTimestamp,
                ReviewedTimestamp = reviewApprovableDataEntity.ReviewedTimestamp,
                ApprovedTimestamp = reviewApprovableDataEntity.ApprovedTimestamp,
                ReviewedCount = reviewApprovableDataEntity.ReviewedCount,
                ApprovedCount = reviewApprovableDataEntity.ApprovedCount,
                EntityReviewApproveState = reviewApprovableDataEntity.EntityReviewApproveState,
                EntityName = reviewApprovableDataEntity.EntityName,
                EntityType = reviewApprovableDataEntity.EntityType,
                LastActionTimestamp = reviewApprovableDataEntity.LastActionTimestamp,
                DataName = reviewApprovableDataEntity.DataName,
                VersionNumber = reviewApprovableDataEntity.VersionNumber
            };
            return reviewApprovableDataEntity16;
        }
    }
}
