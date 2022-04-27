using PerkinElmer.Simplicity.Data.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.Data.Contracts.Migration
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
