using System;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets
{
    public abstract class TargetBase
    {
        public abstract ReleaseVersions TargetReleaseVersion { get; }

        public abstract Version SchemaVersion { get; }

        public abstract TargetTypes TargetType { get; }

        public abstract ITargetBlock<MigrationDataBase> CreateTarget(TargetContextBase targetContext);
    }
}
