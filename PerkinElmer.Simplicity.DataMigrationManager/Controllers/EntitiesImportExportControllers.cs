using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.Data.Contracts.PipelineBuilder;
using PerkinElmer.Simplicity.MigrationManager.Piplelines;

namespace PerkinElmer.Simplicity.MigrationManager.Controllers
{
    public sealed class EntitiesImportExportControllers : MigrationControllerBase
    {
        protected override IDictionary<MigrationDataTypes, PipelineBuilderBase> MigrationPipleLines =>
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

        public override MigrationTypes MigrationType => MigrationTypes.ImportExport;

        public override void Migration(MigrationContextBase migrationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
