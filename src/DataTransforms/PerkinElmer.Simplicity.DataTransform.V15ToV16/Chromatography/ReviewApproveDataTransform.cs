using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class ReviewApproveDataTransform
    {
        public static ReviewApproveVersion16Data Transform(ReviewApproveData reviewApproveData)
        {
            if (reviewApproveData == null) throw new ArgumentNullException(nameof(reviewApproveData));

            var reviewApproveData16 = new ReviewApproveVersion16Data
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
