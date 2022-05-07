using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets
{
    public abstract class TargetContextBase
    {
        public MigrationVersion MigrateToVersion { get; set; }

        public ExecutionDataflowBlockOptions BlockOption { get; set; }

        public abstract TargetType TargetType { get; }
    }
}
