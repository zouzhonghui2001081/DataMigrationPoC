

using PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy
{
    public class DummyDataManager
    {
        public void CreateApplicationDummyDatabase(int projectCount)
        {
            var postgresqlTargetContext = new PostgresqlTargetContext
            {
                ChromatographyConnectionString = "User Id=postgres;Password=Jamun!@#;host=localhost;database=Chromatography15;port=9257",
                SecurityConnectionString = "User Id=postgres;Password=Jamun!@#;host=localhost;database=SimplicityCDSSecurity15;port=9257",
                AuditTrailConnectionString = "User Id=postgres;Password=Jamun!@#;host=localhost;database=SimplicityCDSAuditTrail15;port=9257",
                SystemConnectionString = "User Id=postgres;Password=Jamun!@#;host=localhost;database=postgres;port=9257",
            };

            for (var i = 0; i < projectCount; i++)
            {
                var projectDummyData = new ProjectDummyData();
                var project = projectDummyData.CreateDummyProject(postgresqlTargetContext);
                var projectGuid = project.Guid;

                var analysisResultSetDummyData = new AnalysisResultSetDummyData();
                analysisResultSetDummyData.CreateDummyAnalysisResultSet(postgresqlTargetContext, projectGuid, 20, 200);

                var acqusitionMethodDummyData = new AcqusitionMethodDummyData();
                acqusitionMethodDummyData.CreateDummyAcqusitionMethod(postgresqlTargetContext, projectGuid, 100);

                var processingMethodDummyData = new ProcessingMethodDummyData();
                processingMethodDummyData.CreateDummyProcessingMethod(postgresqlTargetContext, projectGuid, 100);

                var sequenceDummyData = new SequenceDummyData();
                sequenceDummyData.CreateDummySequence(postgresqlTargetContext, projectGuid, 100, 100);

                var reportTemplateDummyData = new ReportTemplateDummyData();
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.ProcessingMethodReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.DetailedCalibrationReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.CondensedCalibrationReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.SummaryReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.SequenceSummaryReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.AcquisitionMethodReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.SequenceDetailedReport, 100);
                reportTemplateDummyData.CreateDummyReportTemplate(postgresqlTargetContext, projectGuid, ReportTemplateCategory.SystemSuitabilitySummaryReport, 100);
            }
        }
    }
}
