using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography
{
    public class ProjectData : Version15DataBase
    {
        public override Version15DataTypes Version15DataTypes => Version15DataTypes.Project;

        public Project Project { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
