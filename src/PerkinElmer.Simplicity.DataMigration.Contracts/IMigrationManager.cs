using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts
{
    public interface IMigrationManager
    {
        IDictionary<MigrationTypes, MigrationControllerBase> MigrationControllers { get; }

        void Migration (MigrationContextBase migrationContext);
    }
}
