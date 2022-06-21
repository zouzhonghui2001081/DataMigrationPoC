
namespace PerkinElmer.Simplicity.Data.Version15.Version.Context
{
    internal class UpgradeSourceContext
    {
        public string ChromatographyConnectionString { get; set; }

        public string SecurityConnectionString { get; set; }

        public string AuditTrailConnectionString { get; set; }

        public bool IsIncludeAuditTrailLog { get; set; }
    }
}
