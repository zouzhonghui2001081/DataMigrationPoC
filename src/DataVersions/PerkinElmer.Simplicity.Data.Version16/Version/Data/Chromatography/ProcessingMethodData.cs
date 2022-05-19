using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography
{
    public class ProcessingMethodData : Version16DataBase
    {
        public ProcessingMethodData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override Version16DataTypes Version16DataTypes => Version16DataTypes.ProcessingMethod;

        public Guid ProjectGuid { get; set; }

        public ProcessingMethod ProcessingMethod { get; set; }

        public ReviewApproveVersion16Data ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
