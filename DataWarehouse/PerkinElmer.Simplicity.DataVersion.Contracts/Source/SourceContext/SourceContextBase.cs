using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Common;

namespace PerkinElmer.Simplicity.Data.Contracts.Source.SourceContext
{
    public abstract class SourceContextBase
    {
        public ReleaseVersions FromReleaseVersion { get; set; }

        public ExecutionDataflowBlockOptions BlockOption { get; set; }

        public abstract SourceTypes SourceType { get; }
    }
}
