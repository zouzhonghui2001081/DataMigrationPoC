using System;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using ReportTemplateData = PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography.ReportTemplateData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class ReportTemplateDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        public static ReportTemplateData Transform(Data.Version15.Contract.Version.Chromatography.ReportTemplateData reportTemplate)
        {
            if(reportTemplate == null) throw new ArgumentNullException(nameof(reportTemplate));
            var reportTemplate16 = new ReportTemplateData
            {
                ProjectGuid = reportTemplate.ProjectGuid,
                ReportTemplate = ReportTemplate.Transform(reportTemplate.ReportTemplate),
            };
            if (reportTemplate.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in reportTemplate.AuditTrailLogs)
                    reportTemplate16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            if (reportTemplate.ReviewApproveData != null)
                reportTemplate16.ReviewApproveData = ReviewApproveDataTransform.Transform(reportTemplate.ReviewApproveData);
            return reportTemplate16;
        }
    }
}
