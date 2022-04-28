

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost
{
    public abstract class SqliteSourceHost : SourceHostBase
    {
        public override SourceTypes SourceType => SourceTypes.Sqlite;
    }
}
