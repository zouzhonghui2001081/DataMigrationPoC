

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost
{
    public abstract class SqliteSourceHost : SourceHostBase
    {
        public override SourceType SourceType => SourceType.Sqlite;
    }
}
