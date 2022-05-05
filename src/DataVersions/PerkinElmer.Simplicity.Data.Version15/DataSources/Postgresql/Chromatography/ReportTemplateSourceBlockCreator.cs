using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
	public class ReportTemplateSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersion SourceVersion => MigrationVersion.Version15;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ReportTemplateMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var reportTemplateDao = new ReportTemplateDao();
                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var reportTemplates = reportTemplateDao.GetList(connection, projectGuid, true);

                                foreach (var reportTemplate in reportTemplates)
                                {
                                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                                        reportTemplate.Id.ToString(), ReviewApproveEntityType.ReportTemplate);
                                    var reportTemplateData = new ReportTemplateMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        ReportTemplate = reportTemplate,
                                        ReviewApproveData = reviewApproveData,
                                        AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate)
                                    };
                                    bufferBlock.Post(reportTemplateData);
                                }
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectEntitiesSourceParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var reportTemplateIds = projectEntitiesParams.EntityGuids;
                                foreach (var reportTemplateId in reportTemplateIds)
                                {
                                    var reportTemplate = reportTemplateDao.Get(connection, projectGuid, reportTemplateId);
                                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                                        reportTemplateId.ToString(), ReviewApproveEntityType.ReportTemplate);

                                    var reportTemplateData = new ReportTemplateMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        ReportTemplate = reportTemplate,
                                        ReviewApproveData = reviewApproveData,
                                        AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate)
                                    };
                                    bufferBlock.Post(reportTemplateData);
                                }
                            }
                            break;
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
    }
}
