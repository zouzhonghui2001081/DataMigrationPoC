using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.DataMigration.Contracts
{
    public interface IMigrationManager
    {
        IDictionary<MigrationType, MigrationControllerBase> MigrationControllers { get; }

        void Migration (MigrationContext migrationContext);
    }
}
