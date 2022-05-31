using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography
{
    public class SequenceData : Version16DataBase
    {
        public SequenceData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override Version16DataTypes Version16DataTypes => Version16DataTypes.Sequence;

        public Guid ProjectGuid { get; set; }

        public Sequence Sequence { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
