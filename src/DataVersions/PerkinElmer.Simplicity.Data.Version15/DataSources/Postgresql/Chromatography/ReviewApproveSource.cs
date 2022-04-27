using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
    public class ReviewApproveSource
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IPropagatorBlock<Guid, ReviewApproveMigrationData> CreateSourceByProjectId(string connectionString, ExecutionDataflowBlockOptions blockOption)
        {
            var bufferBlock = new BufferBlock<ReviewApproveMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectDao = new ProjectDao();
                    var entitySubItemReviewApproveDao = new EntitySubItemReviewApproveDao();
                    var entityReviewApproveDao = new EntityReviewApproveDao();
                    var project = projectDao.GetProject(connection, projectGuid);

                    var reviewApprovableDataEntities = entityReviewApproveDao.GetAll(connection, project.Name, true, 0);

                    foreach (var reviewApprovableDataEntity in reviewApprovableDataEntities)
                    {
                        var approvableDataEntitySubItems = entitySubItemReviewApproveDao.GetAll(connection, reviewApprovableDataEntity.Id);
                        var reviewApprovableData = new ReviewApproveMigrationData
                        {
                            ReviewApprovableDataEntity = reviewApprovableDataEntity,
                            ReviewApprovableDataSubEntities = approvableDataEntitySubItems
                        };
                        bufferBlock.Post(reviewApprovableData);
                    }

                    connection.Close();
                }
            }, blockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get review approve sourcee by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public static ReviewApproveMigrationData GetReviewApproveData(IDbConnection connection, Guid projectId, string entityId, string entityType)
        {

            var projectDao = new ProjectDao();
            var project = projectDao.GetProject(connection, projectId);
            return project != null ? GetReviewApproveData(connection, project.Name, entityId, entityType) : null;
        }

        public static ReviewApproveMigrationData GetReviewApproveData(IDbConnection connection, string projectName,
            string entityId, string entityType)
        {
            var entityReviewApproveDao = new EntityReviewApproveDao();
            var entitySubItemReviewApproveDao = new EntitySubItemReviewApproveDao();
            var reviewApproveEntity = entityReviewApproveDao.Get(connection, projectName, entityId, entityType);
            if (reviewApproveEntity != null)
            {
                var approvableDataEntitySubItems = entitySubItemReviewApproveDao.GetAll(connection, reviewApproveEntity.Id);
                return new ReviewApproveMigrationData
                {
                    ReviewApprovableDataEntity = reviewApproveEntity,
                    ReviewApprovableDataSubEntities = approvableDataEntitySubItems
                };
            }
            return null;
        }
    }
}
