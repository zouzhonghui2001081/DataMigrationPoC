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
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    public class ReportTemplateTarget : TargetBase
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
                if (!(versionData is ReportTemplateMigrationData reportTemplateData)) return;

                if (reportTemplateData.ReportTemplate?.ProjectId == null)
                    throw new ArgumentNullException(nameof(reportTemplateData));
                using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectDao = new ProjectDao();
                    var reportTemplateDao = new ReportTemplateDao();

                    var project = projectDao.GetProject(connection, reportTemplateData.ProjectGuid);
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    if (reportTemplateDao.IsExists(connection, project.Guid, reportTemplateData.ReportTemplate.Name))
                        return;
                    reportTemplateData.ReportTemplate.ProjectId = project.Id;
                    reportTemplateDao.Insert(connection, reportTemplateData.ReportTemplate);
                    if(reportTemplateData.ReviewApproveData != null)
                        ReviewApproveTargets.CreateReviewApproveEntity(connection, reportTemplateData.ReviewApproveData);
                    AuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext, reportTemplateData.AuditTrailLogs);
                    connection.Close();
                }
            }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create report template targets finished with status {_.Status}.");
            });

            return actionBlock;
        }
    }
}
