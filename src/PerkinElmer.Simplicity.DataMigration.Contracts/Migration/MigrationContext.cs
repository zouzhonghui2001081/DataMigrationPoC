using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public enum MigrationType
    {
        Upgrade,
        Archive,
        Retrieve,
        Import,
        Export
    }

    public class MigrationContext
    {
        public MigrationContext(MigrationType migrationType)
        {
            MigrationType = migrationType;
        }

        public MigrationType MigrationType { get; }

        public MigrationDataTypes MigrationDataType { get; set; }

        public SourceContextBase SourceContext { get; set; }

        public TargetContextBase TargetContext { get; set; }

        public TransformContextBase TransformContext { get; set; }
    }
}
