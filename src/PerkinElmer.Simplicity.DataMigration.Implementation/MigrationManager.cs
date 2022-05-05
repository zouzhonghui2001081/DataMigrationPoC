using PerkinElmer.Simplicity.DataMigration.Contracts;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Implementation.Controllers;
using System.Collections.Generic;

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
    }
}
