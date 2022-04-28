
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext
{
    public class UpgradeContext : MigrationContextBase
    {
        public override MigrationTypes MigrationType => MigrationTypes.Upgrade;
    }
}
