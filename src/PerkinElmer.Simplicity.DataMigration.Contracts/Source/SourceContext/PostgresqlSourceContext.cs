
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext
{
    public class PostgresqlSourceContext : SourceContextBase
    {
        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public bool IsMigrateAuditTrail { get; set; }

        public string AuditTrailConnection { get; set; }

        public string ChromatographyConnection { get; set; }

        public bool IsMigrateSecurity { get; set; }

        public string SecurityConnection { get; set; }

        public SourceParamTypes SourceParamType { get; set; }
    }
}
