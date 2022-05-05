﻿using System;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.DataMigration.Implementation.Pipelines;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Sqlite;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Sqlite;

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

        public override IDictionary<MigrationVersion, SourceHostBase> MigrationSourceHost =>
            new Dictionary<MigrationVersion, SourceHostBase>
            {
                {MigrationVersion.Version15, new SqliteSourceHostVer15()},
                {MigrationVersion.Version16, new SqliteSourceHostVer16()}
            };

        public override IDictionary<MigrationVersion, TargetHostBase> MigrationTargetHost =>
            new Dictionary<MigrationVersion, TargetHostBase>
            {
                {MigrationVersion.Version15, new PostgresqlTargetHostVer15()},
                {MigrationVersion.Version16, new PostgresqlTargetHostVer16()}
            };

        public override MigrationType MigrationType => MigrationType.Retrieve;

        public override void Migration(MigrationContext migrationContext)
        {
            if (migrationContext.MigrationType != MigrationType.Retrieve)
                throw new ArgumentException(nameof(migrationContext));

            var sqliteSourceContext = migrationContext.SourceContext as SqliteSourceContext;
            var postgreSqlTargetContext = migrationContext.TargetContext as PostgresqlTargetContext;

            throw new System.NotImplementedException();
        }
    }
}
