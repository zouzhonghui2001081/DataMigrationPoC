using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext
{
    public class PostgresqlSourceContext : SourceContextBase
    {
        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public string AuditTrailConnection { get; set; }

        public string ChromatographyConnection { get; set; }

        public string SecurityConnection { get; set; }
    }
}
