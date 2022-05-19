using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography
{
    public class AcqusitionMethodData : Version16DataBase
    {
        public AcqusitionMethodData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }
        
        public override Version16DataTypes Version16DataTypes => Version16DataTypes.AcqusitionMethod;

        public Guid ProjectGuid { get; set; }

        public AcquisitionMethod AcquisitionMethod { get; set; }

        public ReviewApproveVersion16Data ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
