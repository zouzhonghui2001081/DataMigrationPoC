using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography
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
