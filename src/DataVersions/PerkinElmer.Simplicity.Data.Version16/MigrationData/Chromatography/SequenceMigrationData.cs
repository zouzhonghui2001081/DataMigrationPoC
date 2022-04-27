using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class SequenceMigrationData : MigrationDataBase
    {
        public SequenceMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override ReleaseVersions ReleaseVersion => ReleaseVersions.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.ReportTemplate;

        public Guid ProjectGuid { get; set; }

        public Sequence Sequence { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
