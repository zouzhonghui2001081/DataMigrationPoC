using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines
{
    internal class ProcessingMethodPipelineBuilder : PipelineBuilderBase
    {
        protected override IList<SourceBlockCreatorBase> Sources => new List<SourceBlockCreatorBase>
        {
            new Data.Version15.DataSources.Postgresql.Chromatography.ProcessingMethodSourceBlockCreator(),
            new Data.Version16.DataSources.Postgresql.Chromatography.ProcessingMethodSourceBlockCreator()
        };

        protected override IList<TargetBlockCreatorBase> Targets => new List<TargetBlockCreatorBase>
        {
            new Data.Version15.DataTargets.Postgresql.Chromatography.ProcessingMethodTargetBlockCreator(),
            new Data.Version16.DataTargets.Postgresql.Chromatography.ProcessingMethodTargetBlockCreator()
        };

        protected override IDictionary<MigrationVersion, IDictionary<MigrationVersion, TransformBlockCreatorBase>> TransformMaps => new Dictionary<MigrationVersion, IDictionary<MigrationVersion, TransformBlockCreatorBase>>
        {
            { MigrationVersion.Version15, new Dictionary<MigrationVersion, TransformBlockCreatorBase>
                {
                    { MigrationVersion.Version16, new ProcessingMethodDataTransform() }
                }
            }
        };
    }
}
