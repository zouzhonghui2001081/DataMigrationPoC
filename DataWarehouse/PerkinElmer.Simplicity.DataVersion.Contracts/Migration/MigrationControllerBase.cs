using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.Data.Contracts.PipelineBuilder;

namespace PerkinElmer.Simplicity.Data.Contracts.Migration
{
    public abstract class MigrationControllerBase
    {
        protected abstract IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipleLines { get; }

        public abstract MigrationTypes MigrationType { get; }

        public abstract void Migration(MigrationContextBase migrationContext);
    }
}
