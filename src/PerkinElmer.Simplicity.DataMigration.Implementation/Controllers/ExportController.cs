using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Controllers
{
    public class ExportController : MigrationControllerBase
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
                {MigrationVersions.Version15, new PostgresqlSourceHostVer15()},
                {MigrationVersions.Version16, new PostgresqlSourceHostVer16()}
            };

        protected override IDictionary<MigrationVersions, TargetHostBase> MigrationTargetHost =>
            new Dictionary<MigrationVersions, TargetHostBase>
            {
                {MigrationVersions.Version15, new FileTargetHostVer15()},
                {MigrationVersions.Version16, new FileTargetHostVer16()}
            };

        public override MigrationTypes MigrationType => MigrationTypes.Export;

        public override void Migration(MigrationContextBase migrationContext)
        {
            if (!(migrationContext is ExportContext upgradeMigrationContext))
                throw new ArgumentException();

            var postgresqlSourceContext = upgradeMigrationContext.SourceContext as PostgresqlSourceContext;
            var fileTargetContext = upgradeMigrationContext.TargetContext as FileTargetContext;

            throw new NotImplementedException();
        }
    }
}
