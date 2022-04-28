
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext
{
    public class PostgresqlTargetContext : TargetContextBase
    {
        public override TargetTypes TargetType => TargetTypes.Posgresql;

        public string AuditTrailConnection { get; set; }

        public string ChromatographyConnection { get; set; }

        public string SecurityConnection { get; set; }
    }
}
