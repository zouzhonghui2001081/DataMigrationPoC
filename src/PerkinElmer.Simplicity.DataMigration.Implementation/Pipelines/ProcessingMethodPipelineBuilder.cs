using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines
{
    internal class ProcessingMethodPipelineBuilder : PipelineBuilderBase
    {
        protected override IList<SourceBase> Sources => new List<SourceBase>
        {
            new Data.Version15.DataSources.Postgresql.Chromatography.ProcessingMethodSource(),
            new Data.Version16.DataSources.Postgresql.Chromatography.ProcessingMethodSource()
        };

        protected override IList<TargetBase> Targets => new List<TargetBase>
        {
            new Data.Version15.DataTargets.Postgresql.Chromatography.ProcessingMethodTarget(),
            new Data.Version16.DataTargets.Postgresql.Chromatography.ProcessingMethodTarget()
        };

        protected override IDictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>> TransformMaps => new Dictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>>
        {
            { ReleaseVersions.Version15, new Dictionary<ReleaseVersions, TransformBase>
                {
                    { ReleaseVersions.Version16, new ProcessingMethodDataTransform() }
                }
            }
        };
    }
}
