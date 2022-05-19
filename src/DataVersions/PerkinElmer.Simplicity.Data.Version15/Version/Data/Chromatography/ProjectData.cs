using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography
{
    public class ProjectData : Version15DataBase
    {
        public override Version15DataTypes Version15DataTypes => Version15DataTypes.Project;

        public Project Project { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
