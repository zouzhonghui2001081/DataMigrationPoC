using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext
{
    public abstract class TargetContextBase
    {
        public ReleaseVersions TargetReleaseVersion { get; set; }

        public ExecutionDataflowBlockOptions BlockOption { get; set; }

        public abstract TargetTypes TargetType { get; }
    }
}
