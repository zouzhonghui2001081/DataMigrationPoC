using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public abstract class MigrationControllerBase
    {
        protected abstract IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipelines { get; }

        protected abstract IDictionary<MigrationVersions, SourceHostBase> MigrationSourceHost { get; }

        protected abstract IDictionary<MigrationVersions, TargetHostBase> MigrationTargetHost { get; }

        public abstract MigrationTypes MigrationType { get; }

        public abstract void Migration(MigrationContextBase migrationContext);
    }
}
