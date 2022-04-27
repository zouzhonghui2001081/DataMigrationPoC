using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class ReviewApproveSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ReviewApproveMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
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
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get review approve sourcee by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(SourceContextBase sourceContext)
        {
            throw new NotImplementedException();
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
