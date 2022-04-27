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

        /// <summary>
        /// Create source block for the whole project (Guid)
        /// </summary>
        /// <param name="sourceContext"></param>
        /// <returns></returns>
        public abstract IPropagatorBlock<Guid, MigrationDataBase> CreateProjectSource(SourceContextBase sourceContext);

        /// <summary>
        /// Create source block for the entities (List<Guid>) in a project (Guid)
        /// </summary>
        /// <param name="sourceContext"></param>
        /// <returns></returns>
        public abstract IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateEntitiesSource(SourceContextBase sourceContext);
    }
}
