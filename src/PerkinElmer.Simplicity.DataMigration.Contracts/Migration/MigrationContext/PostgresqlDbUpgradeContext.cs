namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext
{
    public class PostgresqlDbUpgradeMigrationContext : MigrationContextBase
    {
        public override MigrationTypes MigrationType => MigrationTypes.PostgresqlDbUpgrade;
    }
}
