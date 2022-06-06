using System.Data;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    internal class EntityAssociatedReviewApproveTarget
    {
        internal static void CreateReviewApproveEntity(IDbConnection connection, ReviewApproveData reviewApproveData)
        {
            var entityReviewApproveDao = new EntityReviewApproveDao();
            var entitySubItemReviewApproveDao = new EntitySubItemReviewApproveDao();

            entityReviewApproveDao.Create(connection, reviewApproveData.ReviewApprovableDataEntity);
            foreach (var reviewApprovableDataEntitySubItem in reviewApproveData.ReviewApprovableDataSubEntities)
            {
                reviewApprovableDataEntitySubItem.EntityReviewApproveId = reviewApproveData.ReviewApprovableDataEntity.Id;
            }

            entitySubItemReviewApproveDao.Create(connection, reviewApproveData.ReviewApprovableDataSubEntities);
        }
    }
}
