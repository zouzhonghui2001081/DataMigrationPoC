using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager
    {
        private readonly MigrationPipeline _migrationPipeline;

        public MigrationManager(string startVersion, string endVersion, MigrationComponentsFactory migrationComponentsFactory)
        {
            _migrationPipeline = new MigrationPipeline(startVersion, endVersion, migrationComponentsFactory);
        }

        public void CancelMigration()
        {
            _migrationPipeline.Cancellation.Cancel();
        }

        public bool CanStartMigration()
        {
            return _migrationPipeline.SourceBlock != null && 
                   _migrationPipeline.TargetBlock != null;
        }

        public void StartMigration(MigrationContext migrationContext)
        {
            _migrationPipeline.PreparePipeline(migrationContext.TargetConfig);
            _migrationPipeline.StartPipeline(migrationContext.SourceConfig);
            _migrationPipeline.TargetBlock.Completion.Wait();
        }
    }
}
