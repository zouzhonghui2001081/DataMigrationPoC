using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Transform
{
    public abstract class TransformBase
    {
        public abstract ReleaseVersions FromReleaseVersion { get; }

        public abstract ReleaseVersions ToReleaseVersion { get; }

        public abstract TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(
            TransformContextBase transformContext);
    }
}
