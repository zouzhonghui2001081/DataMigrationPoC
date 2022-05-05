using System.Data;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    public class EntityAssociatedReviewApproveTarget
    {
        public static void CreateReviewApproveEntity(IDbConnection connection, ReviewApproveMigrationData reviewApproveData)
        {
            if (reviewApproveData == null) return;
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
