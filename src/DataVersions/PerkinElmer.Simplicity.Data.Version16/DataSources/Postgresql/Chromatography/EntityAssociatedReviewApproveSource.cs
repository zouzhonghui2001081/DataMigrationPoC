using System;
using System.Data;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class EntityAssociatedReviewApproveSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ReviewApproveVersion16Data GetReviewApproveData(IDbConnection connection, Guid projectId, string entityId, string entityType)
        {

            var projectDao = new ProjectDao();
            var project = projectDao.GetProject(connection, projectId);
            return project != null ? GetReviewApproveData(connection, project.Name, entityId, entityType) : null;
        }

        public static ReviewApproveVersion16Data GetReviewApproveData(IDbConnection connection, string projectName,
            string entityId, string entityType)
        {
            var entityReviewApproveDao = new EntityReviewApproveDao();
            var entitySubItemReviewApproveDao = new EntitySubItemReviewApproveDao();
            var reviewApproveEntity = entityReviewApproveDao.Get(connection, projectName, entityId, entityType);
            if (reviewApproveEntity != null)
            {
                var approvableDataEntitySubItems = entitySubItemReviewApproveDao.GetAll(connection, reviewApproveEntity.Id);
                return new ReviewApproveVersion16Data
                {
                    ReviewApprovableDataEntity = reviewApproveEntity,
                    ReviewApprovableDataSubEntities = approvableDataEntitySubItems
                };
            }
            return null;
        }
    }
}
