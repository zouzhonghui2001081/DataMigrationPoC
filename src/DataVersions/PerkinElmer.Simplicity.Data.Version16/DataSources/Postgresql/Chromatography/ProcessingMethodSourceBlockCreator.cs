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
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class ProcessingMethodSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersions SourceVersion => MigrationVersions.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProcessingMethodMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var processingMethodProjDao = new ProcessingMethodProjDao();

                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var processingMethods = processingMethodProjDao.GetAllProcessingMethods(connection, projectGuid);
                                foreach (var processingMethod in processingMethods)
                                {
                                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                                        processingMethod.Guid.ToString(), ReviewApproveEntityType.ProcessingMethod);
                                    var processingMethodData = new ProcessingMethodMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        ProcessingMethod = processingMethod,
                                        ReviewApproveData = reviewApproveData
                                    };
                                    if (posgresqlContext.IsMigrateAuditTrail)
                                        processingMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, processingMethod.Guid.ToString(), EntityTypeConstants.ProcessingMethod);

                                    bufferBlock.Post(processingMethodData);
                                }
                            }
                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectSourceEntitiesParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var processingMethodIds = projectEntitiesParams.EntityGuids;
                                foreach (var processingMethodId in processingMethodIds)
                                {
                                    var processingMethod = processingMethodProjDao.GetProcessingMethod(connection, projectGuid, processingMethodId);
                                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                                        processingMethod.Guid.ToString(), ReviewApproveEntityType.ProcessingMethod);
                                    var processingMethodData = new ProcessingMethodMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        ProcessingMethod = processingMethod,
                                        ReviewApproveData = reviewApproveData
                                    };
                                    if (posgresqlContext.IsMigrateAuditTrail)
                                        processingMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, processingMethod.Guid.ToString(), EntityTypeConstants.ProcessingMethod);

                                    bufferBlock.Post(processingMethodData);
                                }
                            }
                            break;
                    }
                    

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get processing methods source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }
    }
}
