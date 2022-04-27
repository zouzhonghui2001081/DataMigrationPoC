using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Source.SourceContext;

namespace PerkinElmer.Simplicity.Data.Contracts.Source
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
