using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source
{
    public abstract class SourceContextBase
    {
        public MigrationVersion MigrateFromVersion { get; set; }

        public ExecutionDataflowBlockOptions BlockOption { get; set; }

        public abstract SourceTypes SourceType { get; }
    }
}
