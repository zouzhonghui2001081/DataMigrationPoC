using PerkinElmer.Simplicity.DataMigration.Contracts;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Implementation.Controllers;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager : IMigrationManager
    {
        public IDictionary<MigrationTypes, MigrationControllerBase> MigrationControllers =>
            new Dictionary<MigrationTypes, MigrationControllerBase>
            {
                {MigrationTypes.Upgrade, new UpgradeController()},
                {MigrationTypes.Archive, new ArchiveController()},
                {MigrationTypes.Retrieve, new RetrieveController()},
                {MigrationTypes.Import, new ImportController()},
                {MigrationTypes.Export, new ExportController()},
            };

        public void Migration(MigrationContextBase migrationContext)
        {
            var upgradeController = MigrationControllers[migrationContext.MigrationType];
            upgradeController.Migration(migrationContext);
        }
    }
}
