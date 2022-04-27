using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class ProjectMigrationData : MigrationDataBase
    {
        public override ReleaseVersions ReleaseVersion => ReleaseVersions.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.Project;

        public Project Project { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
