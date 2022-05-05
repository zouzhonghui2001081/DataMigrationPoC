using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Controllers
{
    public sealed class UpgradeController : MigrationControllerBase
    {
        protected override IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipelines =>
            new Dictionary<MigrationDataTypes, PipelineBuilderBase>
            {
                {MigrationDataTypes.Project, new ProjectPipelineBuilder()},
                {MigrationDataTypes.AcqusitionMethod, new AcqusitionMethodPipelineBuilder()},
                {MigrationDataTypes.Sequence, new SequencePipelineBuilder()},
                {MigrationDataTypes.AnlysisResultSet, new AnalysisResultSetPipelineBuilder()},
                //{MigrationDataTypes.BatchResultSet, new BatchResultSetPiplelineBuilder()},
                {MigrationDataTypes.CompoundLibrary, new CompoundLibraryPipelineBuilder()},
                {MigrationDataTypes.ProcessingMethod, new ProcessingMethodPipelineBuilder()},
                {MigrationDataTypes.ReportTemplate, new ReportTemplatePipelineBuilder()},
            };

        public override IDictionary<MigrationVersion, SourceHostBase> MigrationSourceHost =>
            new Dictionary<MigrationVersion, SourceHostBase>
            {
                {MigrationVersion.Version15, new PostgresqlSourceHostVer15()},
                {MigrationVersion.Version16, new PostgresqlSourceHostVer16()}
            };

        public override IDictionary<MigrationVersion, TargetHostBase> MigrationTargetHost =>
            new Dictionary<MigrationVersion, TargetHostBase>
            {
                {MigrationVersion.Version15, new PostgresqlTargetHostVer15()},
                {MigrationVersion.Version16, new PostgresqlTargetHostVer16()}
            };

        public override MigrationType MigrationType => MigrationType.Upgrade;

        public override void Migration(MigrationContext migrationContext)
        {
            if (migrationContext.MigrationType != MigrationType.Upgrade)
                throw new ArgumentException();

            var targetHost = MigrationTargetHost[migrationContext.TargetContext.MigrateToVersion];
            targetHost.PrepareTargetHost(migrationContext.TargetContext);

            var sourceHost = MigrationSourceHost[migrationContext.SourceContext.MigrateFromVersion];
            var sourceParams = sourceHost.GetSourceBlockInputParams(migrationContext.SourceContext);
            foreach (var sourceParam in sourceParams)
            {
                if (!(sourceParam is ProjectSourceParams projectSourceParam))
                    continue;
                MigrateProject(projectSourceParam, migrationContext);
            }
        }

        private bool MigrateProject(ProjectSourceParams projectSourceParam, MigrationContext migrationContextBase)
        {
            //Migrate project first.
            var projectPipelineBuilder = MigrationPipelines[MigrationDataTypes.Project];
            var (projectSource, projectTarget) = projectPipelineBuilder.CreateTransformationPipeline(migrationContextBase);
            projectSource.Post(projectSourceParam);
            projectSource.Complete();
            projectTarget.Wait();

            var migrationTasks = new List<Task>();
            var acqusitionMethodPilpeLineBuilder = MigrationPipelines[MigrationDataTypes.AcqusitionMethod];
            var (acqusitionMethodSource, acqusitionMethodTarget) = acqusitionMethodPilpeLineBuilder.CreateTransformationPipeline(migrationContextBase);
            acqusitionMethodSource.Post(projectSourceParam);
            acqusitionMethodSource.Complete();
            //acqusitionMethodTarget.Wait();
            migrationTasks.Add(acqusitionMethodTarget);

            var sequencePilpeLineBuilder = MigrationPipelines[MigrationDataTypes.Sequence];
            var (sequenceSource, sequenceTarget) = sequencePilpeLineBuilder.CreateTransformationPipeline(migrationContextBase);
            sequenceSource.Post(projectSourceParam);
            sequenceSource.Complete();
            //sequenceTarget.Wait();
            migrationTasks.Add(sequenceTarget);

            var anlysisResultSetPipleLineBuilder = MigrationPipelines[MigrationDataTypes.AnlysisResultSet];
            var (analysisResultSetSource, analysisResultSetTarget) = anlysisResultSetPipleLineBuilder.CreateTransformationPipeline(migrationContextBase);
            analysisResultSetSource.Post(projectSourceParam);
            analysisResultSetSource.Complete();
            //analysisResultSetTarget.Wait();
            migrationTasks.Add(analysisResultSetTarget);

            var compoundLibraryPipelineBuilder = MigrationPipelines[MigrationDataTypes.CompoundLibrary];
            var (compoundLibarySource, compoundLibarySourceTarget) = compoundLibraryPipelineBuilder.CreateTransformationPipeline(migrationContextBase);
            compoundLibarySource.Post(projectSourceParam);
            compoundLibarySource.Complete();
            //compoundLibarySourceTarget.Wait();
            migrationTasks.Add(compoundLibarySourceTarget);

            var processingMethodPipelineBuilder = MigrationPipelines[MigrationDataTypes.ProcessingMethod];
            var (processingMethodSource, processingMethodTarget) = processingMethodPipelineBuilder.CreateTransformationPipeline(migrationContextBase);
            processingMethodSource.Post(projectSourceParam);
            processingMethodSource.Complete();
            //processingMethodTarget.Wait();
            migrationTasks.Add(processingMethodTarget);

            var reportTemplatePipelineBuilder = MigrationPipelines[MigrationDataTypes.ReportTemplate];
            var (reportTemplateSource, reportTemplateTarget) = reportTemplatePipelineBuilder.CreateTransformationPipeline(migrationContextBase);
            reportTemplateSource.Post(projectSourceParam);
            reportTemplateSource.Complete();
            //reportTemplateTarget.Wait();
            migrationTasks.Add(reportTemplateTarget);

            Task.WaitAll(migrationTasks.ToArray());

            return true;
        }
    }
}
