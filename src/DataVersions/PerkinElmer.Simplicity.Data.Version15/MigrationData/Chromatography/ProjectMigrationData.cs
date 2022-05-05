using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class ProjectMigrationData : MigrationDataBase
    {
        public override MigrationVersion MigrationVersion => MigrationVersion.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.Project;

        public Project Project { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
