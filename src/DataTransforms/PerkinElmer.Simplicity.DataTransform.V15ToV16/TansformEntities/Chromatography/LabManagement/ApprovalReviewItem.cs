using ApprovalReviewItem15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.LabManagement.ApprovalReviewItem;
using ApprovalReviewItem16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.LabManagement.ApprovalReviewItem;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.LabManagement
{
    public class ApprovalReviewItem
    {
        public static ApprovalReviewItem16 Transform(ApprovalReviewItem15 approvalReviewItem)
        {
            if (approvalReviewItem == null) return null;
            return new ApprovalReviewItem16
            {
                Id = approvalReviewItem.Id,
                Guid = approvalReviewItem.Guid,
                Name = approvalReviewItem.Name,
                DisplayOrder = approvalReviewItem.DisplayOrder,
                IsApprovalReviewOn = approvalReviewItem.IsApprovalReviewOn,
                IsSubmitReviewApprove = approvalReviewItem.IsSubmitReviewApprove,
                IsSubmitApprove = approvalReviewItem.IsSubmitApprove,
                CreatedDate = approvalReviewItem.CreatedDate,
                CreatedUserId = approvalReviewItem.CreatedUserId,
                ModifiedDate = approvalReviewItem.ModifiedDate,
                ModifiedUserId = approvalReviewItem.ModifiedUserId
            };
        }
    }
}
