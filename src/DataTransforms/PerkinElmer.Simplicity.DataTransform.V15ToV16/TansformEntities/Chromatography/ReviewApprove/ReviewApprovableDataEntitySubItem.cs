using ReviewApprovableDataEntitySubItem15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove.ReviewApprovableDataEntitySubItem;
using ReviewApprovableDataEntitySubItem16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove.ReviewApprovableDataEntitySubItem;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ReviewApprove
{
    public class ReviewApprovableDataEntitySubItem
    {
        public static ReviewApprovableDataEntitySubItem16 Transform(
            ReviewApprovableDataEntitySubItem15 reviewApprovableDataEntitySubItem)
        {
            if (reviewApprovableDataEntitySubItem == null) return null;
            return new ReviewApprovableDataEntitySubItem16
            {
                Id = reviewApprovableDataEntitySubItem.Id,
                ProjectName = reviewApprovableDataEntitySubItem.ProjectName,
                ProjectId = reviewApprovableDataEntitySubItem.ProjectId,
                EntityReviewApproveId = reviewApprovableDataEntitySubItem.EntityReviewApproveId,
                EntitySubItemId = reviewApprovableDataEntitySubItem.EntitySubItemId,
                EntitySubItemName = reviewApprovableDataEntitySubItem.EntitySubItemName,
                EntitySubItemType = reviewApprovableDataEntitySubItem.EntitySubItemType,
                EntitySubItemReviewApproveState = reviewApprovableDataEntitySubItem.EntitySubItemReviewApproveState,
                EntitySubItemSampleReportTemplate = reviewApprovableDataEntitySubItem.EntitySubItemSampleReportTemplate,
                EntitySubItemSummaryReportGroup = reviewApprovableDataEntitySubItem.EntitySubItemSummaryReportGroup,
                ReviewApproveComment = reviewApprovableDataEntitySubItem.ReviewApproveComment
            };
        }
    }
}
