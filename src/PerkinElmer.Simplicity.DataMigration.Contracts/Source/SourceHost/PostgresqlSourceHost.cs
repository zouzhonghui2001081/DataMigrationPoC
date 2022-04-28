

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost
{
    public abstract class PostgresqlSourceHost : SourceHostBase
    {
        public override SourceTypes SourceType => SourceTypes.Posgresql;
    }
}
 
