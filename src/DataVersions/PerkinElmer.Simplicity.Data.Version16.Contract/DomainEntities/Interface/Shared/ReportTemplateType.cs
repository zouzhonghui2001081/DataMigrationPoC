using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    [Flags]
    public enum ReportTemplateType
    {
        None = 1,
        Sample = 2,
        Summary = 4,
        DetialedCalibration = 8,
        CondensedCalibration = 16,
        SequenceSummary = 32,
        AcquisitionMethod = 64,
        ProcessingMethod = 128,
        SequenceDetail = 256,
        Big = 512,
        Mini = 1024,
        SSTSummary = 2048,
        DetailedSystemSuitabilityReport = 4056,
    }
    public static class ReportTemplateTypeExtensions
    {
        public static string GetDescription(this ReportTemplateType reportType)
        {
            switch (reportType)
            {
                case ReportTemplateType.None:
                    return "None";
                case ReportTemplateType.Sample:
                    return "Sample Report";
                case ReportTemplateType.Summary:
                    return "Summary Report";
                case ReportTemplateType.DetialedCalibration:
                    return "Detailed Calibration Report";
                case ReportTemplateType.CondensedCalibration:
                    return "Condensed Calibration Report";
                case ReportTemplateType.SequenceSummary:
                    return "Sequence Summary Report";
                case ReportTemplateType.SequenceDetail:
                    return "Sequence Detailed Report";
                case ReportTemplateType.AcquisitionMethod:
                    return "Acquisition Method Report";
                case ReportTemplateType.ProcessingMethod:
                    return "Processing Method Report";
                case ReportTemplateType.Mini:
                    return "Mini Report";
                case ReportTemplateType.SSTSummary:
                    return "System Suitability Summary Report";
                case ReportTemplateType.DetailedSystemSuitabilityReport:
                    return "Detailed System Suitability Report";
                default:
                    return "Big Report";
            }
        }
        public static ReportTemplateType ToReportTemplateType(this string reportType)
        {
            switch(reportType)
            {
                case "Sample Report":
                    return ReportTemplateType.Sample;
                case "Summary Report":
                    return ReportTemplateType.Summary;
                case "Detailed Calibration Report":
                    return ReportTemplateType.DetialedCalibration;
                case "Condensed Calibration Report":
                    return ReportTemplateType.CondensedCalibration;
                case "Sequence Summary Report":
                    return ReportTemplateType.SequenceSummary;
                case "Sequence Detailed Report":
                    return ReportTemplateType.SequenceDetail;
                case "Acquisition Method Report":
                    return ReportTemplateType.AcquisitionMethod;
                case "Processing Method Report":
                    return ReportTemplateType.ProcessingMethod;
                case "Mini Report":
                    return ReportTemplateType.Mini;
                case "System Suitability Summary Report":
                    return ReportTemplateType.SSTSummary;
                case "Detailed System Suitability Report":
                    return ReportTemplateType.DetailedSystemSuitabilityReport;
                default:
                    return ReportTemplateType.Big;
            }
        }
    }
}
