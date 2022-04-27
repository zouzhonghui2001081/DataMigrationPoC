using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Contracts;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.MigrationManager.Controllers;

namespace PerkinElmer.Simplicity.MigrationManager
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
