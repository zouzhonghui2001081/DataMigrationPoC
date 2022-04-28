using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class ReviewApproveSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersions SourceVersion => MigrationVersions.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ReviewApproveMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectDao = new ProjectDao();
                    var entitySubItemReviewApproveDao = new EntitySubItemReviewApproveDao();
                    var entityReviewApproveDao = new EntityReviewApproveDao();

                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
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
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectSourceEntitiesParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                throw new NotImplementedException();
                            }

                            break;
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
