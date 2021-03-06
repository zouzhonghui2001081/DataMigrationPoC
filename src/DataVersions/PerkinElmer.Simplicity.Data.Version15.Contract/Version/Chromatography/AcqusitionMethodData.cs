using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography
{
    public class AcqusitionMethodData : Version15DataBase
    {
        public AcqusitionMethodData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override Version15DataTypes Version15DataTypes => Version15DataTypes.AcqusitionMethod;

        public Guid ProjectGuid { get; set; }

        public AcquisitionMethod AcquisitionMethod { get; set; }

        public ReviewApproveData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
        
    }
}
