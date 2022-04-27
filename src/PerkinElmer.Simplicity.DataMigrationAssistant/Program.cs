using log4net;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContextFactory;
using PerkinElmer.Simplicity.DataMigration.Implementation;
using System.Reflection;

namespace PerkinElmer.Simplicity.DataMigrationAssistant
{
    class Program
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var upgradeTargetVersion = ReleaseVersions.Version16;
            var postgresqlDbUpgradeContextFactory = new PostgresqlDbUpgradeContextFactory(upgradeTargetVersion);
            var dbUpgradeContext = postgresqlDbUpgradeContextFactory.GetMigrationContext();

            var migrationManager = new MigrationManager();
            migrationManager.Migration(dbUpgradeContext);
        }
    }
}
