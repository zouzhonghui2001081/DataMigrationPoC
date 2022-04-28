
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext
{
    public class ImportContext : MigrationContextBase
    {
        public override MigrationTypes MigrationType => MigrationTypes.Import;
    }
}
