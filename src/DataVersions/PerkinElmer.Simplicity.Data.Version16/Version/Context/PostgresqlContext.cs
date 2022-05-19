
namespace PerkinElmer.Simplicity.Data.Version16.Version.Context
{
    public class PostgresqlContext
    {    
        public string AuditTrailConnection { get; set; }

        public string ChromatographyConnection { get; set; }

        public string SecurityConnection { get; set; }

        public bool IsMigrateAuditTrail { get; set; }

        public bool IsMigrateSecurity { get; set; }
    }
}
