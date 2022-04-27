using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContextFactory;

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

            var migrationManager = new MigrationManager.MigrationManager();
            migrationManager.Migration(dbUpgradeContext);
        }
    }
}
