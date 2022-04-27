using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.Data.Contracts.Source;
using PerkinElmer.Simplicity.Data.Contracts.Targets;
using PerkinElmer.Simplicity.Data.Contracts.Transform;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography;

namespace PerkinElmer.Simplicity.MigrationManager.Piplelines
{
    internal class BatchResultSetPipelineBuilder : PipelineBuilderBase
    {
        protected override IList<SourceBase> Sources => new List<SourceBase>
        {
            new Data.Version15.DataSources.Postgresql.Chromatography.BatchResultSetSource(),
            new Data.Version16.DataSources.Postgresql.Chromatography.BatchResultSetSource()
        };

        protected override IList<TargetBase> Targets => new List<TargetBase>
        {
            new Data.Version15.DataTargets.Postgresql.Chromatography.BatchResultSetTarget(),
            new Data.Version16.DataTargets.Postgresql.Chromatography.BatchResultSetTarget()
        };

        protected override IDictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>> TransformMaps => new Dictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>>
        {
            { ReleaseVersions.Version15, new Dictionary<ReleaseVersions, TransformBase>
                {
                    { ReleaseVersions.Version16, new BatchResultSetDataTransform() }
                }
            }
        };
    }
}
