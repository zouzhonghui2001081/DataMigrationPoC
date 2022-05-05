using System;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Controllers
{
    public sealed class RetrieveController : MigrationControllerBase
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

        protected override IDictionary<MigrationVersions, SourceHostBase> MigrationSourceHost =>
            new Dictionary<MigrationVersions, SourceHostBase>
            {
                {MigrationVersions.Version15, new SqliteSourceHostVer15()},
                {MigrationVersions.Version16, new SqliteSourceHostVer16()}
            };

        protected override IDictionary<MigrationVersions, TargetHostBase> MigrationTargetHost =>
            new Dictionary<MigrationVersions, TargetHostBase>
            {
                {MigrationVersions.Version15, new PostgresqlTargetHostVer15()},
                {MigrationVersions.Version16, new PostgresqlTargetHostVer16()}
            };

        public override MigrationTypes MigrationType => MigrationTypes.Retrieve;

        public override void Migration(MigrationContextBase migrationContext)
        {
            if (!(migrationContext is RetrieveContext retrieveContext))
                throw new ArgumentException(nameof(migrationContext));

            var sqliteSourceContext = retrieveContext.SourceContext as SqliteSourceContext;
            var postgreSqlTargetContext = retrieveContext.TargetContext as PostgresqlTargetContext;

            throw new System.NotImplementedException();
        }
    }
}
