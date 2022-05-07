
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext
{
    public class PostgresqlTargetContext : TargetContextBase
    {
        public override TargetType TargetType => TargetType.Posgresql;

        public bool IsMigrateAuditTrail { get; set; }

        public string AuditTrailConnection { get; set; }

        public string ChromatographyConnection { get; set; }

        public bool IsMigrateSecurity { get; set; }

        public string SecurityConnection { get; set; }
    }
}
