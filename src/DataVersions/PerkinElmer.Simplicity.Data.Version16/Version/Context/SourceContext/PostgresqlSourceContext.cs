
namespace PerkinElmer.Simplicity.Data.Version16.Version.Context.SourceContext
{
    internal sealed class PostgresqlSourceContext
    {
        public string ChromatographyConnectionString { get; set; }

        public string SecurityConnectionString { get; set; }

        public string AuditTrailConnectionString { get; set; }

        public string SystemConnectionString { get; set; }

        public bool IsIncludeAuditTrail { get; set; }
    }
}
