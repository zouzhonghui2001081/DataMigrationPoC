
namespace PerkinElmer.Simplicity.Data.Version15.Version.Context
{
    internal class MigrationSourceContext
    {
        public string MigrationType { get; set; }

        public string ArchiveProjectGuid { get; set; }

        public string RetrieveFileLocation { get; set; }

        public bool IsIncludeAuditTrailLog { get; set; }
    }
}
