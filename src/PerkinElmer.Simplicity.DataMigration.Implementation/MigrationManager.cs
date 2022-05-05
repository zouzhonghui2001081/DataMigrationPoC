using PerkinElmer.Simplicity.DataMigration.Contracts;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContextFactory;
using PerkinElmer.Simplicity.DataMigration.Implementation.Controllers;
using System.Collections.Generic;
using System.Threading;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager : IMigrationManager
    {
        public IDictionary<MigrationType, MigrationControllerBase> MigrationControllers =>
            new Dictionary<MigrationType, MigrationControllerBase>
            {
                {MigrationType.Upgrade, new UpgradeController()},
                {MigrationType.Archive, new ArchiveController()},
                {MigrationType.Retrieve, new RetrieveController()},
                {MigrationType.Import, new ImportController()},
                {MigrationType.Export, new ExportController()},
            };

        public void Migration(MigrationContext migrationContext)
        {
            var upgradeController = MigrationControllers[migrationContext.MigrationType];
            upgradeController.Migration(migrationContext);
        }

        public void Migration(MigrationType migrationType, MigrationVersion fromVersion, MigrationVersion toVersion, CancellationTokenSource cancellationTokenSource)
        {
            var controller = MigrationControllers[migrationType];
            var migrationContext = GetMigrationContext(migrationType, fromVersion, toVersion, cancellationTokenSource, controller);
            controller.Migration(migrationContext);
        }

        private static MigrationContext GetMigrationContext(MigrationType migrationType,
            MigrationVersion fromVersion,
            MigrationVersion toVersion,
            CancellationTokenSource cancellationTokenSource,
            MigrationControllerBase controller)
        {
            switch(migrationType)
            {
                case MigrationType.Upgrade:
                    var postgresqlDbUpgradeContextFactory = new PostgresqlDbUpgradeContextFactory(fromVersion, toVersion, cancellationTokenSource, controller);
                    return postgresqlDbUpgradeContextFactory.GetMigrationContext();
                case MigrationType.Retrieve:
                case MigrationType.Archive:
                case MigrationType.Import:
                case MigrationType.Export:
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
