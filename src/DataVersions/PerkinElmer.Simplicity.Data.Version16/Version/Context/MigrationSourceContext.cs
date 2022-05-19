
namespace PerkinElmer.Simplicity.Data.Version16.Version.Context
{
    public class MigrationSourceContext
    {
        public string MigrationType { get; set; }

        public string ArchiveProjectGuid { get; set; }

        public string RetrieveFileLocation { get; set; }

        public bool IsIncludeAuditTrailLog { get; set; }
    }
}
