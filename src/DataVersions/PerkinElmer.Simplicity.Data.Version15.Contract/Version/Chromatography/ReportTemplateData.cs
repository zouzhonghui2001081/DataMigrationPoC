using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography
{
    public class ReportTemplateData : Version15DataBase
    {
        public ReportTemplateData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override Version15DataTypes Version15DataTypes => Version15DataTypes.ReportTemplate;

        public Guid ProjectGuid { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public ReviewApproveData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
