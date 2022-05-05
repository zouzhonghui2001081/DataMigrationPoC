using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class ProjectMigrationData : MigrationDataBase
    {
        public override MigrationVersion MigrationVersion => MigrationVersion.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.Project;

        public Project Project { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
