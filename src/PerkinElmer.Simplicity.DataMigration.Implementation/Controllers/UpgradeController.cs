using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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

        private readonly IDictionary<MigrationVersions, PostgresqlSourceHost> _postgresqlSourceHosts =
            new Dictionary<MigrationVersions, PostgresqlSourceHost>
            {
                {MigrationVersions.Version15, new PostgresqlSourceHostVer15()},
                {MigrationVersions.Version16, new PostgresqlSourceHostVer16()}
            };

        private readonly IDictionary<MigrationVersions, PostgresqlTargetHost> _postgresqlTargetHosts =
            new Dictionary<MigrationVersions, PostgresqlTargetHost>
            {
                {MigrationVersions.Version15, new PostgresqlTargetHostVer15()},
                {MigrationVersions.Version16, new PostgresqlTargetHostVer16()}
            };

        public override MigrationTypes MigrationType => MigrationTypes.Upgrade;

        public override void Migration(MigrationContextBase migrationContext)
        {
            if (!(migrationContext is UpgradeContext upgradeMigrationContext))
                throw new ArgumentException();

            var targetHost = _postgresqlTargetHosts[migrationContext.TargetContext.TargetMigrationVersion];
            targetHost.PrepareTargetHost(migrationContext.TargetContext);

            var sourceHost = _postgresqlSourceHosts[migrationContext.SourceContext.FromMigrationVersion];
            var sourceParams = sourceHost.GetSourceBlockInputParams(migrationContext.SourceContext);
            foreach (var sourceParam in sourceParams)
            {
                if (!(sourceParam is ProjectSourceParams projectSourceParam))
                    continue;
                MigrateProject(projectSourceParam, migrationContext);
            }
        }

        private bool MigrateProject(ProjectSourceParams projectSourceParam, MigrationContextBase migrationContextBase)
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
