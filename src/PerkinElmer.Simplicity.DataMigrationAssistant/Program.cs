﻿using log4net;
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
            var upgradeTargetVersion = MigrationVersions.Version16;
            var cancellationTokenSource = new CancellationTokenSource();

            var postgresqlDbUpgradeContextFactory = new PostgresqlDbUpgradeContextFactory(upgradeTargetVersion, cancellationTokenSource);
            var dbUpgradeContext = postgresqlDbUpgradeContextFactory.GetMigrationContext();

            var migrationManager = new MigrationManager();
            migrationManager.Migration(dbUpgradeContext);
        }
    }
}
