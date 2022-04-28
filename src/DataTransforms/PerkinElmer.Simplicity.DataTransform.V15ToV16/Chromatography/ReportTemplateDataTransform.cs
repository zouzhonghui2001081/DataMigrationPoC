using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class ReportTemplateDataTransform : TransformBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override MigrationVersions FromVersion => MigrationVersions.Version15;

        public override MigrationVersions ToVersion => MigrationVersions.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var reportTemplateTransform = new TransformBlock<MigrationDataBase, MigrationDataBase>(fromVersionData=>
            {
                if (fromVersionData.MigrationVersion != MigrationVersions.Version15 ||
                    !(fromVersionData is Data.Version15.MigrationData.Chromatography.ReportTemplateMigrationData reportTemplateData))
                    throw new ArgumentException("From version data is incorrect!");
                return Transform(reportTemplateData);
            }, transformContext.BlockOption);
            reportTemplateTransform.Completion.ContinueWith(_ =>
            {
                Log.Info($"report templates transform complete with State{_.Status}");
            });
            return reportTemplateTransform;
        }

        internal static ReportTemplateMigrationData Transform(Data.Version15.MigrationData.Chromatography.ReportTemplateMigrationData reportTemplateMigration)
        {
            if(reportTemplateMigration == null) throw new ArgumentNullException(nameof(reportTemplateMigration));
            var reportTemplate16 = new ReportTemplateMigrationData
            {
                ProjectGuid = reportTemplateMigration.ProjectGuid,
                ReportTemplate = ReportTemplate.Transform(reportTemplateMigration.ReportTemplate),
            };
            if (reportTemplateMigration.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in reportTemplateMigration.AuditTrailLogs)
                    reportTemplate16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            if (reportTemplateMigration.ReviewApproveData != null)
                reportTemplate16.ReviewApproveData = ReviewApproveDataTransform.Transform(reportTemplateMigration.ReviewApproveData);
            return reportTemplate16;
        }
    }
}
