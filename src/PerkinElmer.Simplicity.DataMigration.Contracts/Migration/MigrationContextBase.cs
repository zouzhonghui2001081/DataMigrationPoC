using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public enum MigrationTypes
    {
        PostgresqlDbUpgrade,
        ArchiveRetrieve,
        ImportExport
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
