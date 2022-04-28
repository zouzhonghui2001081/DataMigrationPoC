using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Transform
{
    public abstract class TransformBlockCreatorBase
    {
        public abstract MigrationVersions FromVersion { get; }

        public abstract MigrationVersions ToVersion { get; }

        public abstract TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(
            TransformContextBase transformContext);
    }
}
