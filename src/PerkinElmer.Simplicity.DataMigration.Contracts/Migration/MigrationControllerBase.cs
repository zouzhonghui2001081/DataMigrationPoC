using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public abstract class MigrationControllerBase
    {
        protected abstract IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipelines { get; }

        public abstract IDictionary<MigrationVersion, SourceHostBase> MigrationSourceHost { get; }

        public abstract IDictionary<MigrationVersion, TargetHostBase> MigrationTargetHost { get; }

        public abstract MigrationType MigrationType { get; }

        public abstract void Migration(MigrationContext migrationContext);
    }
}
