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
    public class ReportTemplateSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateProjectSource(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ReportTemplateMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var reportTemplateDao = new ReportTemplateDao();
                    var reportTemplates = reportTemplateDao.GetList(connection, projectGuid, true);

                    foreach (var reportTemplate in reportTemplates)
                    {
                        var reviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                            reportTemplate.Id.ToString(), ReviewApproveEntityType.ReportTemplate);

                        var reportTemplateData = new ReportTemplateMigrationData
                        {
                            ProjectGuid = projectGuid,
                            ReportTemplate = reportTemplate,
                            ReviewApproveData = reviewApproveData,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate)
                        };
                        bufferBlock.Post(reportTemplateData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get report templates source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateEntitiesSource(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ReportTemplateMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    connection.Close();
                    var projectGuid = parameters.Item1;
                    var reportTemplateIds = parameters.Item2;
                    var reportTemplateDao = new ReportTemplateDao();
                    foreach (var reportTemplateId in reportTemplateIds)
                    {
                        var reportTemplate = reportTemplateDao.Get(connection, projectGuid, reportTemplateId);
                        var reviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                            reportTemplateId.ToString(), ReviewApproveEntityType.ReportTemplate);

                        var reportTemplateData = new ReportTemplateMigrationData
                        {
                            ProjectGuid = projectGuid,
                            ReportTemplate = reportTemplate,
                            ReviewApproveData = reviewApproveData,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate)
                        };
                        bufferBlock.Post(reportTemplateData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get report templates by project id and report template id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }
    }
}
