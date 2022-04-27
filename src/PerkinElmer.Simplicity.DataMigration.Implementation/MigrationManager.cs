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
                {MigrationTypes.PostgresqlDbUpgrade, new PostgresqlDbUpgradeController()},
                {MigrationTypes.ArchiveRetrieve, new ProjectArchiveRetrieveControllers()},
                {MigrationTypes.ImportExport, new EntitiesImportExportControllers()}
            };

        public void Migration(MigrationContextBase migrationContext)
        {
            var upgradeController = MigrationControllers[MigrationTypes.PostgresqlDbUpgrade];
            upgradeController.Migration(migrationContext);
        }
    }
}
