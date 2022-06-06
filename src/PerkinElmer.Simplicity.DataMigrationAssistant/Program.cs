using log4net;
using System.Reflection;
using System.Runtime.Loader;
using PerkinElmer.Simplicity.DataMigration.Implementation;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigrationAssistant
{
    class Program
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var version15ContractPath = @"C:\DEV\DataMigrationPoC\Output\Debug\PerkinElmer.Simplicity.Data.Version15.Contract.dll";
            var version16ContractPath = @"C:\DEV\DataMigrationPoC\Output\Debug\PerkinElmer.Simplicity.Data.Version16.Contract.dll";
            AssemblyLoadContext.Default.LoadFromAssemblyPath(version15ContractPath);
            AssemblyLoadContext.Default.LoadFromAssemblyPath(version16ContractPath);
            var migrationContext = new MigrationContext
            {
                SourceConfig = "{\"MigrationType\":\"Upgrade\",\"ArchiveProjectGuid\":null,\"RetrieveFileLocation\":null,\"IsIncludeAuditTrailLog\":true}",
                TargetConfig = "{\"MigrationType\":\"Upgrade\",\"ArchiveFileLocation\":null}"
            };
            var migrationComponentsFactory = new MigrationComponentsFactory();
            var migrationManager = new MigrationManager("Version15", "Version16", migrationComponentsFactory);
            migrationManager.StartMigration(migrationContext);
        }
    }
}
