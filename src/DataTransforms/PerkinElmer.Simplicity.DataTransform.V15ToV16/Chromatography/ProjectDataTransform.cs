using System;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using ProjectData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.ProjectData;
using ProjectData16 = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.ProjectData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class ProjectDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ProjectData16 Transform(ProjectData15 projectData)
        {
            if (projectData == null) throw new ArgumentNullException(nameof(projectData));

            return new ProjectData16
            {
                Project = Project.Transform(projectData.Project),
                AuditTrailLogs = null
            };
        }
    }
}
