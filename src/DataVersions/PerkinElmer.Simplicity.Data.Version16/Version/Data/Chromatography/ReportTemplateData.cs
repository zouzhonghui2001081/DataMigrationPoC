using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography
{
    public class ReportTemplateData : Version16DataBase
    {
        public ReportTemplateData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }
        public override Version16DataTypes Version16DataTypes => Version16DataTypes.ReportTemplate;

        public Guid ProjectGuid { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public ReviewApproveVersion16Data ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
