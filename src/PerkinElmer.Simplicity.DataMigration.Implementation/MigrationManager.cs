using System;
using System.Linq;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager
    {
        private readonly string _startVersion;

        private readonly string _endVersion;

        private readonly MigrationPipeline _migrationPipeline;

        public MigrationManager(string startVersion, string endVersion)
        {
            _startVersion = startVersion;
            _endVersion = endVersion;
            _migrationPipeline = new MigrationPipeline(startVersion, endVersion);
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
            _migrationPipeline.TargetBlock.PrepareTarget(_endVersion, migrationContext.TargetConfig);
            _migrationPipeline.SourceBlock.StartSourceDataflow(_startVersion, migrationContext.SourceConfig);
        }
    }
}
