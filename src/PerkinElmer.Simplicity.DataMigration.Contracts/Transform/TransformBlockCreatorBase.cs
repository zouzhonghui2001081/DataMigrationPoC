using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Transform
{
    public abstract class TransformBlockCreatorBase
    {
        public abstract MigrationVersion FromVersion { get; }

        public abstract MigrationVersion ToVersion { get; }

        public abstract TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(
            TransformContextBase transformContext);
    }
}
