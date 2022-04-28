using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public enum MigrationTypes
    {
        Upgrade,
        Archive,
        Retrieve,
        Import,
        Export
    }

    public abstract class MigrationContextBase
    {
        public abstract MigrationTypes MigrationType { get; }

        public MigrationDataTypes MigrationDataType { get; set; }

        public SourceContextBase SourceContext { get; set; }

        public TargetContextBase TargetContext { get; set; }

        public TransformContextBase TransformContext { get; set; }
    }
}
