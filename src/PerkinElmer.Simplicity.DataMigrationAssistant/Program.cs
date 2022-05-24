using log4net;
using System.Reflection;
using PerkinElmer.Simplicity.DataMigration.Implementation;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigrationAssistant
{
    class Program
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var migrationContext = new MigrationContext
            {
                SourceConfig = "{\"MigrationType\":\"Upgrade\",\"ArchiveProjectGuid\":null,\"RetrieveFileLocation\":null,\"IsIncludeAuditTrailLog\":true}",
                TargetConfig = "{\"MigrationType\":\"Upgrade\",\"ArchiveFileLocation\":null}"
            };
            var migrationManager = new MigrationManager("Version15", "Version16");
            migrationManager.Start(migrationContext);
        }
    }
}
