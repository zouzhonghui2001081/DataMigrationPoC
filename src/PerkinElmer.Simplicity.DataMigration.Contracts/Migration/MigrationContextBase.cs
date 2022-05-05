using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{

    public class MigrationContext
    {
        public MigrationDataTypes MigrationDataType { get; set; }

        public SourceContextBase SourceContext { get; set; }

        public TargetContextBase TargetContext { get; set; }

        public TransformContextBase TransformContext { get; set; }
    }
}
