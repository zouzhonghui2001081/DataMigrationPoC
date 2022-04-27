using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Common.Postgresql;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Targets;
using PerkinElmer.Simplicity.Data.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    public class ProcessingMethodTarget : TargetBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override TargetTypes TargetType => TargetTypes.Posgresql;

        public override ReleaseVersions TargetReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override ITargetBlock<MigrationDataBase> CreateTarget(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            var actionBlock = new ActionBlock<MigrationDataBase>(versionData =>
            {
                if (versionData == null || versionData.MigrationDataTypes != MigrationDataTypes.ProcessingMethod) return;
                if (versionData.ReleaseVersion != ReleaseVersions.Version16) return;
                if (!(versionData is ProcessingMethodMigrationData processingMethodData)) return;

                using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    CreateProcessingMethod(connection, processingMethodData.ProjectGuid, processingMethodData.ProcessingMethod);
                    ReviewApproveTargets.CreateReviewApproveEntity(connection, processingMethodData.ReviewApproveData);
                    AuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext, processingMethodData.AuditTrailLogs);
                    connection.Close();
                }
            }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create processing method targets finished with status {_.Status}.");
            });

            return actionBlock;
        }

        public static long CreateProcessingMethod(IDbConnection connection, Guid projectId, ProcessingMethod processingMethod)
        {
            var projectDao = new ProjectDao();
            var processingMethodProjDao = new ProcessingMethodProjDao();
            var projectToProcessingMethodMapDao = new ProjectToProcessingMethodMapDao();

            var project = projectDao.GetProject(connection, projectId);
            if (project == null) throw new ArgumentNullException(nameof(project));
            if (processingMethodProjDao.IsExistsProcessingMethod(connection, project.Name, processingMethod.Name, processingMethod.Guid))
                return -1;
            processingMethodProjDao.Create(connection, processingMethod);

            var map = new ProjectToProcessingMethodMap()
            {
                ProcessingMethodId = processingMethod.Id,
                ProjectId = project.Id
            };
            projectToProcessingMethodMapDao.Create(connection, map);

            return processingMethod.Id;
        }

        public static long CreateProcessingMethod(IDbConnection connection, long batchResultSetId, ProcessingMethod processingMethod)
        {
            var processingMethodBatchResultDao = new ProcessingMethodBatchResultDao();
            processingMethodBatchResultDao.Create(connection, batchResultSetId, processingMethod);
            return processingMethod.Id;
        }

        public static long CreateModifiableProcessingMethod(IDbConnection connection, long analyisiResultSetId, ProcessingMethod modifiableProcessingMethod)
        {
            var processingMethodModifiableDao = new ProcessingMethodModifiableDao();
            processingMethodModifiableDao.Create(connection, analyisiResultSetId, modifiableProcessingMethod);
            return modifiableProcessingMethod.Id;
        }
    }
}
