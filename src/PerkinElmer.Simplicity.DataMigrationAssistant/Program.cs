using log4net;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContextFactory;
using PerkinElmer.Simplicity.DataMigration.Implementation;
using System.Reflection;
using System.Threading;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigrationAssistant
{
    class Program
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var upgradeFromVersion = MigrationVersion.Version15;
            var upgradeToVersion = MigrationVersion.Version16;
            var cancellationTokenSource = new CancellationTokenSource();

            var migrationManager = new MigrationManager();
            migrationManager.Migration( MigrationType.Upgrade, upgradeFromVersion, upgradeToVersion, cancellationTokenSource);
        }
    }
}
