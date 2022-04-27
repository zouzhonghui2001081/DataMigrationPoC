using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source
{
    public abstract class SourceBase
    {
        public abstract ReleaseVersions SourceReleaseVersion { get; }

        public abstract Version SchemaVersion { get; }

        public abstract SourceTypes SourceType { get; }

        public abstract IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext);

        public abstract IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(SourceContextBase sourceContext);
    }
}
