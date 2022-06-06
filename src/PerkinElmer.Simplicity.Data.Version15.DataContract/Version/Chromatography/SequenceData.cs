using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography
{
    public class SequenceData : Version15DataBase
    {
        public SequenceData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override Version15DataTypes Version15DataTypes => Version15DataTypes.Sequence;

        public Guid ProjectGuid { get; set; }

        //TODO: Consider List<Sequence> Sequences
        public Sequence Sequence { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
