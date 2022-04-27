using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class ProjectDataTransform : TransformBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override ReleaseVersions FromReleaseVersion => ReleaseVersions.Version15;

        public override ReleaseVersions ToReleaseVersion => ReleaseVersions.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var projectTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(fromVersionData =>
            {
                if (fromVersionData.ReleaseVersion != ReleaseVersions.Version15 ||
                    !(fromVersionData is Data.Version15.MigrationData.Chromatography.ProjectMigrationData projectData))
                    throw new ArgumentException("From version data is incorrect!");
                return new ProjectMigrationData
                {
                    Project = Project.Transform(projectData.Project),
                    AuditTrailLogs = null
                };
            }, transformContext.BlockOption);
            projectTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"project transform complete with State{_.Status}");
            });
            return projectTransformBlock;
        }
    }
}
