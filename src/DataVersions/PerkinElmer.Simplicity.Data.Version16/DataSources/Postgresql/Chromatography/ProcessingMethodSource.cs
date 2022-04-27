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
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class ProcessingMethodSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProcessingMethodMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var processingMethodProjDao = new ProcessingMethodProjDao();
                    var processingMethods = processingMethodProjDao.GetAllProcessingMethods(connection, projectGuid);

                    foreach (var processingMethod in processingMethods)
                    {
                        var reviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                            processingMethod.Guid.ToString(), ReviewApproveEntityType.ProcessingMethod);
                        var processingMethodData = new ProcessingMethodMigrationData
                        {
                            ProjectGuid = projectGuid,
                            ProcessingMethod = processingMethod,
                            ReviewApproveData = reviewApproveData,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, processingMethod.Guid.ToString(), EntityTypeConstants.ProcessingMethod)
                        };
                        bufferBlock.Post(processingMethodData);
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

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProcessingMethodMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                var projectGuid = parameters.Item1;
                var processingMethodIds = parameters.Item2;
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var processingMethodProjDao = new ProcessingMethodProjDao();
                    foreach (var processingMethodId in processingMethodIds)
                    {
                        var processingMethod = processingMethodProjDao.GetProcessingMethod(connection, projectGuid, processingMethodId);
                        var reviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                            processingMethod.Guid.ToString(), ReviewApproveEntityType.ProcessingMethod);
                        var processingMethodData = new ProcessingMethodMigrationData
                        {
                            ProjectGuid = projectGuid,
                            ProcessingMethod = processingMethod,
                            ReviewApproveData = reviewApproveData,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, processingMethod.Guid.ToString(), EntityTypeConstants.ProcessingMethod)
                        };
                        bufferBlock.Post(processingMethodData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get processing methods by processing method ids finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }
    }
}
