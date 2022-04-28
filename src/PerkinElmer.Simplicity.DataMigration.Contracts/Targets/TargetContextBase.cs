using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets
{
    public abstract class TargetContextBase
    {
        public MigrationVersions TargetMigrationVersion { get; set; }

        public ExecutionDataflowBlockOptions BlockOption { get; set; }

        public abstract TargetTypes TargetType { get; }
    }
}
