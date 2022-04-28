using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines
{
    internal class ProjectPipelineBuilder : PipelineBuilderBase
    {
        protected override IList<SourceBlockCreatorBase> Sources => new List<SourceBlockCreatorBase>
        {
            new Data.Version15.DataSources.Postgresql.Chromatography.ProjectSourceBlockCreator(),
            new Data.Version16.DataSources.Postgresql.Chromatography.ProjectSourceBlockCreator()
        };

        protected override IList<TargetBlockCreatorBase> Targets => new List<TargetBlockCreatorBase>
        {
            new Data.Version15.DataTargets.Postgresql.Chromatography.ProjectTargetBlockCreator(),
            new Data.Version16.DataTargets.Postgresql.Chromatography.ProjectTargetBlockCreator()
        };

        protected override IDictionary<MigrationVersions, IDictionary<MigrationVersions, TransformBlockCreatorBase>> TransformMaps => new Dictionary<MigrationVersions, IDictionary<MigrationVersions, TransformBlockCreatorBase>>
        {
            { MigrationVersions.Version15, new Dictionary<MigrationVersions, TransformBlockCreatorBase>
                {
                    { MigrationVersions.Version16, new ProjectDataTransform() }
                }
            }
        };
    }
}
