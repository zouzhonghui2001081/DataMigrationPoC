using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.Data.Contracts.Source;
using PerkinElmer.Simplicity.Data.Contracts.Targets;
using PerkinElmer.Simplicity.Data.Contracts.Transform;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography;

namespace PerkinElmer.Simplicity.MigrationManager.Piplelines
{
    internal class SequencePipelineBuilder : PipelineBuilderBase
    {
        protected override IList<SourceBase> Sources => new List<SourceBase>
        {
            new Data.Version15.DataSources.Postgresql.Chromatography.SequenceSource(),
            new Data.Version16.DataSources.Postgresql.Chromatography.SequenceSource()
        };

        protected override IList<TargetBase> Targets => new List<TargetBase>
        {
            new Data.Version15.DataTargets.Postgresql.Chromatography.SequenceTarget(),
            new Data.Version16.DataTargets.Postgresql.Chromatography.SequenceTarget()
        };

        protected override IDictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>> TransformMaps => new Dictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>>
        {
            { ReleaseVersions.Version15, new Dictionary<ReleaseVersions, TransformBase>
                {
                    { ReleaseVersions.Version16, new SequenceDataTransform() }
                }
            }
        };
    }
}
