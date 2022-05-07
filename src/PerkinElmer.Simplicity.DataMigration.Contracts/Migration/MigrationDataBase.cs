namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public abstract class MigrationDataBase
    {
        public abstract MigrationVersion MigrationVersion { get; }

        public abstract MigrationDataTypes MigrationDataTypes { get; }
    }
}
