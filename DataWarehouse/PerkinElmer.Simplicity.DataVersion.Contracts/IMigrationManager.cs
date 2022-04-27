using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContext;

namespace PerkinElmer.Simplicity.Data.Contracts
{
    public interface IMigrationManager
    {
        IDictionary<MigrationTypes, MigrationControllerBase> MigrationControllers { get; }

        void Migration (MigrationContextBase migrationContext);
    }
}
