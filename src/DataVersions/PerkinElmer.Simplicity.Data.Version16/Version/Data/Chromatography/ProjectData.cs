using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography
{
    public class ProjectData : Version16DataBase
    {
        public override Version16DataTypes Version16DataTypes => Version16DataTypes.Project;

        public Project Project { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
