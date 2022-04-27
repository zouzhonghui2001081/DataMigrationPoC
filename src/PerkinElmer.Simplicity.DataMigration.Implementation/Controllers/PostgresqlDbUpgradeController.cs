using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ProjectDao15 = PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography.ProjectDao;
using ProjectDao16 = PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography.ProjectDao;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Controllers
{
    public sealed class PostgresqlDbUpgradeController : MigrationControllerBase
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

        public override MigrationTypes MigrationType => MigrationTypes.PostgresqlDbUpgrade;

        public override void Migration(MigrationContextBase migrationContext)
        {
            var resetChromatographyDatabase = new ResetChromatographyDatabase();
            var resetAuditTrailDatabase = new ResetAuditTrailDatabase();
            resetChromatographyDatabase.Reset(Mode.Hard, migrationContext.TargetContext.TargetReleaseVersion);
            resetAuditTrailDatabase.Reset(Mode.Hard, migrationContext.TargetContext.TargetReleaseVersion);

            var postgresqlSourceContext = migrationContext.SourceContext as PostgresqlSourceContext;
            var projectGuids = GetAllProjectGuid(postgresqlSourceContext);
            foreach (var projectGuid in projectGuids)
                MigrateProject(projectGuid, migrationContext);
        }
        

        private bool MigrateProject(Guid projectGuid, MigrationContextBase migrationContextBase)
        {
            //Migrate project first.
            var projectPipelineBuilder = MigrationPipelines[MigrationDataTypes.Project];
            var (projectSource, projectTarget) = projectPipelineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            projectSource.Post(projectGuid);
            projectSource.Complete();
            projectTarget.Wait();

            var migrationTasks = new List<Task>();
            var acqusitionMethodPilpeLineBuilder = MigrationPipelines[MigrationDataTypes.AcqusitionMethod];
            var (acqusitionMethodSource, acqusitionMethodTarget) = acqusitionMethodPilpeLineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            acqusitionMethodSource.Post(projectGuid);
            acqusitionMethodSource.Complete();
            //acqusitionMethodTarget.Wait();
            migrationTasks.Add(acqusitionMethodTarget);

            var sequencePilpeLineBuilder = MigrationPipelines[MigrationDataTypes.Sequence];
            var (sequenceSource, sequenceTarget) = sequencePilpeLineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            sequenceSource.Post(projectGuid);
            sequenceSource.Complete();
            //sequenceTarget.Wait();
            migrationTasks.Add(sequenceTarget);

            var anlysisResultSetPipleLineBuilder = MigrationPipelines[MigrationDataTypes.AnlysisResultSet];
            var (analysisResultSetSource, analysisResultSetTarget) = anlysisResultSetPipleLineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            analysisResultSetSource.Post(projectGuid);
            analysisResultSetSource.Complete();
            //analysisResultSetTarget.Wait();
            migrationTasks.Add(analysisResultSetTarget);

            var compoundLibraryPipelineBuilder = MigrationPipelines[MigrationDataTypes.CompoundLibrary];
            var (compoundLibarySource, compoundLibarySourceTarget) = compoundLibraryPipelineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            compoundLibarySource.Post(projectGuid);
            compoundLibarySource.Complete();
            //compoundLibarySourceTarget.Wait();
            migrationTasks.Add(compoundLibarySourceTarget);

            var processingMethodPipelineBuilder = MigrationPipelines[MigrationDataTypes.ProcessingMethod];
            var (processingMethodSource, processingMethodTarget) = processingMethodPipelineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            processingMethodSource.Post(projectGuid);
            processingMethodSource.Complete();
            //processingMethodTarget.Wait();
            migrationTasks.Add(processingMethodTarget);

            var reportTemplatePipelineBuilder = MigrationPipelines[MigrationDataTypes.ReportTemplate];
            var (reportTemplateSource, reportTemplateTarget) = reportTemplatePipelineBuilder.CreateProjectTransformationPipeline(migrationContextBase);
            reportTemplateSource.Post(projectGuid);
            reportTemplateSource.Complete();
            //reportTemplateTarget.Wait();
            migrationTasks.Add(reportTemplateTarget);

            Task.WaitAll(migrationTasks.ToArray());

            return true;
        }

        private IList<Guid> GetAllProjectGuid(PostgresqlSourceContext sourceContext)
        {
            var projectGuid = new List<Guid>();
            using (var dbConnection = new NpgsqlConnection(sourceContext.ChromatographyConnection))
            {
                switch (sourceContext.FromReleaseVersion)
                {
                    case ReleaseVersions.Version15:
                        var projectDao15 = new ProjectDao15();
                        var projects15 = projectDao15.GetAllProjects(dbConnection);
                        foreach (var project in projects15)
                            projectGuid.Add(project.Guid);
                        break;
                    case ReleaseVersions.Version16:
                        var projectDao16 = new ProjectDao16();
                        var projects16 = projectDao16.GetAllProjects(dbConnection);
                        foreach (var project in projects16)
                            projectGuid.Add(project.Guid);
                        break;
                }
            }

            return projectGuid;
        }
    }
}
