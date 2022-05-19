using log4net;
using System.Reflection;
using PerkinElmer.Simplicity.Data.Version16;
using PerkinElmer.Simplicity.DataMigration.Implementation;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigrationAssistant
{
    class Program
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Version16Host.PreparePostgresqlHost();
            var migrationManager = new MigrationManager(VersionNames.Version15, VersionNames.Version16);
            migrationManager.StartDataFlow(string.Empty);
        }
    }
}
