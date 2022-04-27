using System;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Targets.TargetContext;

namespace PerkinElmer.Simplicity.Data.Contracts.Targets
{
    public abstract class TargetBase
    {
        public abstract ReleaseVersions TargetReleaseVersion { get; }

        public abstract Version SchemaVersion { get; }

        public abstract TargetTypes TargetType { get; }

        public abstract ITargetBlock<MigrationDataBase> CreateTarget(TargetContextBase targetContext);
    }
}
