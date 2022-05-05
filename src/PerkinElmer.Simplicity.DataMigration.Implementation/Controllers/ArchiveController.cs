
using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Controllers
{
    public class ArchiveController : MigrationControllerBase
    {
        protected override IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipelines =>
            new Dictionary<MigrationDataTypes, PipelineBuilderBase>
            {
                {MigrationDataTypes.Project, new ProjectPipelineBuilder()},
                {MigrationDataTypes.AcqusitionMethod, new AcqusitionMethodPipelineBuilder()},
                {MigrationDataTypes.AnlysisResultSet, new AnalysisResultSetPipelineBuilder()},
                {MigrationDataTypes.BatchResultSet, new BatchResultSetPipelineBuilder()},
                {MigrationDataTypes.CompoundLibrary, new CompoundLibraryPipelineBuilder()},
                {MigrationDataTypes.ProcessingMethod, new ProcessingMethodPipelineBuilder()},
                {MigrationDataTypes.ReportTemplate, new ReportTemplatePipelineBuilder()},
            };

        protected override IDictionary<MigrationVersion, SourceHostBase> MigrationSourceHost =>
            new Dictionary<MigrationVersion, SourceHostBase>
            {
                {MigrationVersion.Version15, new PostgresqlSourceHostVer15()},
                {MigrationVersion.Version16, new PostgresqlSourceHostVer16()}
            };

        protected override IDictionary<MigrationVersion, TargetHostBase> MigrationTargetHost =>
            new Dictionary<MigrationVersion, TargetHostBase>
            {
                {MigrationVersion.Version15, new SqliteTargetHostVer15()},
                {MigrationVersion.Version16, new SqliteTargetHostVer16()}
            };

        public override MigrationType MigrationType => MigrationType.Archive;

        public override void Migration(MigrationContext migrationContext)
        {
            if (migrationContext.MigrationType != MigrationType.Archive)
                throw new ArgumentException(nameof(migrationContext));

            var postgreSqlSourceContext = migrationContext.SourceContext as PostgresqlSourceContext;
            var sqliteTargetContext = migrationContext.TargetContext as SqliteTargetContext;

            throw new System.NotImplementedException();
        }
    }
}
