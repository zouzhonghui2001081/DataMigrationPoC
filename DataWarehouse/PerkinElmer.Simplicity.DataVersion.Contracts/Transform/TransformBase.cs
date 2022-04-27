using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.Data.Contracts.Transform
{
    public abstract class TransformBase
    {
        public abstract ReleaseVersions FromReleaseVersion { get; }

        public abstract ReleaseVersions ToReleaseVersion { get; }

        public abstract TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(
            TransformContextBase transformContext);
    }
}
