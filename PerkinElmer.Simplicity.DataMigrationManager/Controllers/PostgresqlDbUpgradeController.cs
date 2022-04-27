using System;
using PerkinElmer.Simplicity.MigrationManager.Piplelines;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Npgsql;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Common.Postgresql.Utils;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.Data.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.Data.Contracts.Source.SourceContext;
using ProjectDao15 = PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography.ProjectDao;
using ProjectDao16 = PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography.ProjectDao;

namespace PerkinElmer.Simplicity.MigrationManager.Controllers
{
    public sealed class PostgresqlDbUpgradeController : MigrationControllerBase
    {
        protected override IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipleLines =>
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
            var projectPipleLineBuilder = MigrationPipleLines[MigrationDataTypes.Project];
            var (projectSource, projectTarget) = projectPipleLineBuilder.CreateProjectPipleline(migrationContextBase);
            projectSource.Post(projectGuid);
            projectSource.Complete();
            projectTarget.Wait();

            var migrationTasks = new List<Task>();
            var acqusitionMethodPilpeLineBuilder = MigrationPipleLines[MigrationDataTypes.AcqusitionMethod];
            var (acqusitionMethodSource, acqusitionMethodTarget) = acqusitionMethodPilpeLineBuilder.CreateProjectPipleline(migrationContextBase);
            acqusitionMethodSource.Post(projectGuid);
            acqusitionMethodSource.Complete();
            //acqusitionMethodTarget.Wait();
            migrationTasks.Add(acqusitionMethodTarget);

            var sequencePilpeLineBuilder = MigrationPipleLines[MigrationDataTypes.Sequence];
            var (sequenceSource, sequenceTarget) = sequencePilpeLineBuilder.CreateProjectPipleline(migrationContextBase);
            sequenceSource.Post(projectGuid);
            sequenceSource.Complete();
            //sequenceTarget.Wait();
            migrationTasks.Add(sequenceTarget);

            var anlysisResultSetPipleLineBuilder = MigrationPipleLines[MigrationDataTypes.AnlysisResultSet];
            var (analysisResultSetSource, analysisResultSetTarget) = anlysisResultSetPipleLineBuilder.CreateProjectPipleline(migrationContextBase);
            analysisResultSetSource.Post(projectGuid);
            analysisResultSetSource.Complete();
            //analysisResultSetTarget.Wait();
            migrationTasks.Add(analysisResultSetTarget);

            var compoundLibraryPipleLineBuilder = MigrationPipleLines[MigrationDataTypes.CompoundLibrary];
            var (compoundLibarySource, compoundLibarySourceTarget) = compoundLibraryPipleLineBuilder.CreateProjectPipleline(migrationContextBase);
            compoundLibarySource.Post(projectGuid);
            compoundLibarySource.Complete();
            //compoundLibarySourceTarget.Wait();
            migrationTasks.Add(compoundLibarySourceTarget);

            var processingMethodPipleLineBuilder = MigrationPipleLines[MigrationDataTypes.ProcessingMethod];
            var (processingMethodSource, processingMethodTarget) = processingMethodPipleLineBuilder.CreateProjectPipleline(migrationContextBase);
            processingMethodSource.Post(projectGuid);
            processingMethodSource.Complete();
            //processingMethodTarget.Wait();
            migrationTasks.Add(processingMethodTarget);

            var reportTemplatePipleLineBuilder = MigrationPipleLines[MigrationDataTypes.ReportTemplate];
            var (reportTemplateSource, reportTemplateTarget) = reportTemplatePipleLineBuilder.CreateProjectPipleline(migrationContextBase);
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
