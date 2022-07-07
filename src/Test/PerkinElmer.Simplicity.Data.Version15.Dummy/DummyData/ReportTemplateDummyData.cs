using System;
using System.Data;
using System.IO;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Reporting;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class ReportTemplateCategory
    {
        public const string ProcessingMethodReport = "Processing Method Report";

        public const string DetailedCalibrationReport = "Detailed Calibration Report";

        public const string CondensedCalibrationReport = "Condensed Calibration Report";

        public const string SummaryReport = "Summary Report";

        public const string SequenceSummaryReport = "Sequence Summary Report";

        public const string AcquisitionMethodReport = "Acquisition Method Report";

        public const string SequenceDetailedReport = "Sequence Detailed Report";

        public const string SystemSuitabilitySummaryReport = "System Suitability Summary Report";
    }

    internal class ReportTemplateDummyData
    {
        private const string AcqusitionMethodReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.AcquisitionMethodReport.json";

        private const string CondensedCalibrationReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.CondensedCalibrationReport.json";

        private const string DetailedCalibrationReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.DetailedCalibrationReport.json";

        private const string ProcessingMethodReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.ProcessingMethodReport.json";

        private const string SequenceDetailedReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.SequenceDetailedReport.json";

        private const string SequenceSummaryReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.SequenceSummaryReport.json";

        private const string SummaryReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.SummaryReport.json";

        private const string SystemSuitabilitySummaryReportTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Reports.SystemSuitabilitySummaryReport.json";

        public void CreateDummyReportTemplate(PostgresqlTargetContext postgresqlTargetContext, Guid projectGuid, string reportTemplateCategory,
            int reportTemplateCount)
        {
            var reportTemplateJson = GetReportTemplate(reportTemplateCategory);
            var reportTemplateDomain = JsonConverter.FromJson<IReportTemplate>(reportTemplateJson);
            var reportTemplateName = reportTemplateCategory + " ";
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();

            for (var i = 1; i <= reportTemplateCount; i++)
            {
                reportTemplateDomain.Id = Guid.NewGuid();
                reportTemplateDomain.Name = reportTemplateName + i.ToString("0000") + " " + Guid.NewGuid().ToString().Substring(0, 8);
                var reportTemplateEntity = new ReportTemplate();
                DomainContractAdaptor.PopulateReportTemplateEntity(reportTemplateDomain, reportTemplateEntity);
                var reportTemplateData = new ReportTemplateData
                {
                    ProjectGuid = projectGuid,
                    ReportTemplate = reportTemplateEntity
                };
                ReportTemplateTarget.SaveReportTemplate(reportTemplateData, postgresqlTargetContext);
            }
        }

        private string GetReportTemplate(string reportTemplateCategory)
        {
            var assembly = typeof(AcqusitionMethodDummyData).Assembly;

            var reportTemplateResource = string.Empty;
            switch (reportTemplateCategory)
            {
                case ReportTemplateCategory.ProcessingMethodReport:
                    reportTemplateResource = ProcessingMethodReportTemplate;
                    break;
                case ReportTemplateCategory.DetailedCalibrationReport:
                    reportTemplateResource = DetailedCalibrationReportTemplate;
                    break;
                case ReportTemplateCategory.CondensedCalibrationReport:
                    reportTemplateResource = CondensedCalibrationReportTemplate;
                    break;
                case ReportTemplateCategory.SummaryReport:
                    reportTemplateResource = SummaryReportTemplate;
                    break;
                case ReportTemplateCategory.SequenceSummaryReport:
                    reportTemplateResource = SequenceSummaryReportTemplate;
                    break;
                case ReportTemplateCategory.AcquisitionMethodReport:
                    reportTemplateResource = AcqusitionMethodReportTemplate;
                    break;
                case ReportTemplateCategory.SequenceDetailedReport:
                    reportTemplateResource = SequenceDetailedReportTemplate;
                    break;
                case ReportTemplateCategory.SystemSuitabilitySummaryReport:
                    reportTemplateResource = SystemSuitabilitySummaryReportTemplate;
                    break;
            }

            if (string.IsNullOrEmpty(reportTemplateResource)) return string.Empty;

            using var stream = assembly.GetManifestResourceStream(reportTemplateResource);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                    $"Failed to load resource {reportTemplateResource}"));
            return reader.ReadToEnd();
        }
    }
}
