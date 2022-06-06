using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography
{
    public class ProcessingMethodData : Version15DataBase
    {
        public ProcessingMethodData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override Version15DataTypes Version15DataTypes => Version15DataTypes.ProcessingMethod;

        public Guid ProjectGuid { get; set; }

        public ProcessingMethod ProcessingMethod { get; set; }

        public ReviewApproveData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
