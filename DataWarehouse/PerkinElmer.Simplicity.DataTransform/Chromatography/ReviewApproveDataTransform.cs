using System;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ReviewApprove;
using ReviewApproveData15 = PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography.ReviewApproveMigrationData;
using ReviewApproveData16 = PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography.ReviewApproveMigrationData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class ReviewApproveDataTransform
    {
        public static ReviewApproveData16 Transform(ReviewApproveData15 reviewApproveData)
        {
            if (reviewApproveData == null) throw new ArgumentNullException(nameof(reviewApproveData));

            var reviewApproveData16 = new ReviewApproveData16
            {
                ProjectGuid = reviewApproveData.ProjectGuid,
                ReviewApprovableDataEntity = ReviewApprovableDataEntity.Transform(reviewApproveData.ReviewApprovableDataEntity),
            };
            foreach (var reviewApprovableDataSubEntity in reviewApproveData.ReviewApprovableDataSubEntities)
                reviewApproveData16.ReviewApprovableDataSubEntities.Add(ReviewApprovableDataEntitySubItem.Transform(reviewApprovableDataSubEntity));

            return reviewApproveData16;
        }
    }
}
