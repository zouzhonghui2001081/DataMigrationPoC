using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public abstract class MigrationControllerBase
    {
        protected abstract IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipelines { get; }

        public abstract MigrationTypes MigrationType { get; }

        public abstract void Migration(MigrationContextBase migrationContext);
    }
}
