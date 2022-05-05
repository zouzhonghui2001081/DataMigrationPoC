using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using System.Collections.Generic;
using System.Threading;

namespace PerkinElmer.Simplicity.DataMigration.Contracts
{
    public interface IMigrationManager
    {
        void Migration (MigrationType migrationType, MigrationVersion toVersion, CancellationTokenSource cancellationTokenSource);
    }
}
