using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext
{
    public abstract class SourceContextBase
    {
        public ReleaseVersions FromReleaseVersion { get; set; }

        public ExecutionDataflowBlockOptions BlockOption { get; set; }

        public abstract SourceTypes SourceType { get; }
    }
}
