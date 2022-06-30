using System;
using System.IO;
using log4net;
using System.Reflection;
using System.Runtime.Loader;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.DataMigration.Implementation;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigrationAssistant
{
    class Program
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            LoadSharedLibraries();
            var migrationContext = new MigrationContext
            {
                SourceConfig = GenerateSourceConfig("Version15"),
                TargetConfig = GenerateTargetConfig("Version16")
            };
            var migrationComponentsFactory = new MigrationComponentsFactory();
            var migrationManager = new MigrationManager("Version15", "Version16", migrationComponentsFactory);
            migrationManager.StartMigration(migrationContext);

        }

        static void LoadSharedLibraries()
        {
            var exeFileFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var sharedFileFolder = Path.Combine(exeFileFolder, "Share");
            var sharedLibraryFiles = System.IO.Directory.GetFiles(sharedFileFolder, "*.dll");
            foreach(var sharedLibraryFile in sharedLibraryFiles)
                AssemblyLoadContext.Default.LoadFromAssemblyPath(sharedLibraryFile);
        }

        static string GenerateSourceConfig(string version)
        {
            if (version != "Version15") throw new ArgumentException("Not support version!");
            var sourceConfig = new JObject
            {
                new JProperty("MigrationType", "Upgrade"),
                new JProperty("Payload", new JObject
                {
                    new JProperty("ChromatographyConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=Chromatography15;port=9257"),
                    new JProperty("AuditTrailConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=SimplicityCDSAuditTrail15;port=9257"),
                    new JProperty("SecurityConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=SimplicityCDSSecurity15;port=9257"),
                    new JProperty("SystemConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=postgres;port=9257"),
                    new JProperty("IsIncludeAuditTrail", bool.TrueString)
                }.ToString())
            };
            return sourceConfig.ToString();
        }

        static string GenerateTargetConfig(string version)
        {
            if (version != "Version16") throw new ArgumentException("Not support version!");
            var sourceConfig = new JObject
            {
                new JProperty("MigrationType", "Upgrade"),
                new JProperty("Payload", new JObject
                {
                    new JProperty("ChromatographyConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=Chromatography16;port=9257"),
                    new JProperty("AuditTrailConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=SimplicityCDSAuditTrail16;port=9257"),
                    new JProperty("SecurityConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=SimplicityCDSSecurity16;port=9257"),
                    new JProperty("SystemConnectionString",
                        "User Id=postgres;Password=Jamun!@#;host=localhost;database=postgres;port=9257")
                }.ToString())
            };
            return sourceConfig.ToString();
        }
    }
}
