using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContextFactory
{
    internal enum PostgresqlDatabases
    {
        Chromatography,
        AuditTrail,
        Security
    }

    public class PostgresqlDbUpgradeContextFactory : ContextFactocyBase
    {
        private readonly MigrationVersions _toVersion;
        private readonly CancellationTokenSource _cancellationTokenSource;

        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PostgresqlDbUpgradeContextFactory(MigrationVersions toVersion, CancellationTokenSource cancellationTokenSource)
        {
            _toVersion = toVersion;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public override MigrationContext GetMigrationContext()
        {
            var sourceContext = GeneratePostgresqlSourceContext();
            var targetContext = GeneratePostgresqlTargetContext();
            var transformContext = GeneratePostgresqlTransformContext();
            var migrationContext = new MigrationContext
            {
                SourceContext = sourceContext,
                TargetContext = targetContext,
                TransformContext = transformContext
            };

            return migrationContext;
        }

        private PostgresqlSourceContext GeneratePostgresqlSourceContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = _cancellationTokenSource.Token
            };


            var chromatographySchemaVersion = GetSchemaVersion(PostgresqlDatabases.Chromatography);
            var migrationVersion = GetChromatographyMigrationVersion(chromatographySchemaVersion);

            var auditTrailSchemaVersion = GetSchemaVersion(PostgresqlDatabases.AuditTrail);
            var securityschemaVersion = GetSchemaVersion(PostgresqlDatabases.Security);
            

            if (migrationVersion == MigrationVersions.Unknown)
                throw new ArgumentException("Failed to get release version from CDS application database! ");

            return new PostgresqlSourceContext
            {
                BlockOption = blockOption,
                FromMigrationVersion = migrationVersion,
                SourceParamType = Source.SourceParamTypes.ProjectGuid,
                ChromatographyConnection = GetChromatographyDatabaseConn(chromatographySchemaVersion),
                AuditTrailConnection = GetAuditTrailDatabaseConn(auditTrailSchemaVersion),
                SecurityConnection = GetSecurityDatabaseConn(securityschemaVersion)
            };

        }

        private PostgresqlTargetContext GeneratePostgresqlTargetContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = _cancellationTokenSource.Token
            };
            switch (_toVersion)
            {
                case MigrationVersions.Version15:
                    var chromatographyConnNameV15 = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer15];
                    var auditTrailConnNameV15 = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer15];
                    var securityConnNameV15 = ConfigurationManager.AppSettings[ConstNames.SecurityConnVer15];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        TargetMigrationVersion = _toVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV15].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV15].ConnectionString,
                        SecurityConnection = ConfigurationManager.ConnectionStrings[securityConnNameV15].ConnectionString
                    };
                case MigrationVersions.Version16:
                    var chromatographyConnNameV16 = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer16];
                    var auditTrailConnNameV16 = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer16];
                    var securityConnNameV16 = ConfigurationManager.AppSettings[ConstNames.SecurityConnVer16];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        TargetMigrationVersion = _toVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV16].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV16].ConnectionString,
                        SecurityConnection = ConfigurationManager.ConnectionStrings[securityConnNameV16].ConnectionString,
                    };
            }

            throw new ArgumentException("Target Version not supported!");
        }

        private TransformContext GeneratePostgresqlTransformContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4 ,
                CancellationToken = _cancellationTokenSource.Token
            };
            return new TransformContext
            {
                BlockOption = blockOption
            };
        }

        private MigrationVersions GetChromatographyMigrationVersion(Version schemaVersion)
        {
            if (schemaVersion == SchemaVersions.ChromatographySchemaVersion15)
                return MigrationVersions.Version15;

            if (schemaVersion == SchemaVersions.ChromatographySchemaVersion16)
                return MigrationVersions.Version16;

            return MigrationVersions.Unknown;
        }

        private Version GetSchemaVersion(PostgresqlDatabases database )
        {
            try
            {
                var connectionStringName = ConfigurationManager.AppSettings[ConstNames.PostgresqlDefaultDb];
                var defaultConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                var defaultDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(defaultConnectionString);

                var databaseConnName = string.Empty;
                switch (database)
                {
                    case PostgresqlDatabases.Chromatography:
                        databaseConnName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConn];
                        break;
                    case PostgresqlDatabases.AuditTrail:
                        databaseConnName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConn];
                        break;
                    case PostgresqlDatabases.Security:
                        databaseConnName = ConfigurationManager.AppSettings[ConstNames.SecurityConn];
                        break;
                }

                var configuredConnName = ConfigurationManager.AppSettings[databaseConnName];
                var connectionString = ConfigurationManager.ConnectionStrings[configuredConnName].ConnectionString;
                var appDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(connectionString);

                using (var connection = new NpgsqlConnection(defaultDbConnectionStrBuilder.ConnectionString))
                {
                    connection.Open();

                    var databaseExists = connection.ExecuteScalar<bool>(
                        $"SELECT EXISTS(SELECT 1 FROM pg_catalog.pg_database where datname = '{appDbConnectionStrBuilder.Database}');");
                    connection.Close();

                    if (databaseExists == false)
                        throw new ArgumentException("Chromatogram database not exist!");
                }

                using (IDbConnection connection = new NpgsqlConnection(appDbConnectionStrBuilder.ConnectionString))
                {
                    var databaseVersion =
                        connection.QueryFirstOrDefault(
                            $"SELECT * FROM {ConstNames.SchemaTableName} WHERE {ConstNames.MajorVersionColumn} != -1;");

                    var dbSchemaVer = new Version((int)databaseVersion.majorversion,
                        (int)databaseVersion.minorversion);
                    connection.Close();
                    return dbSchemaVer;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return null;
        }

        private string GetChromatographyDatabaseConn(Version schemaVersion)
        {
            if (schemaVersion == SchemaVersions.ChromatographySchemaVersion15)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer15];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            if (schemaVersion == SchemaVersions.ChromatographySchemaVersion16)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer16];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            throw new ArgumentException(nameof(schemaVersion));
        }

        private bool IsNeedMigrateAuditTrail(Version schemaVersion)
        {
            switch (_toVersion)
            {
                case MigrationVersions.Version16:
                    if (schemaVersion == SchemaVersions.AuditTrailSchemaVersion15)
                        return true;
                    break;
            }
            return false;
        }

        private string GetAuditTrailDatabaseConn(Version schemaVersion)
        {
            if (schemaVersion == SchemaVersions.AuditTrailSchemaVersion15)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer15];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            if (schemaVersion == SchemaVersions.AuditTrailSchemaVersion16)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer16];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            throw new ArgumentException(nameof(schemaVersion));
        }

        private bool IsNeedMigrateSecurity(Version schemaVersion)
        {
            switch (_toVersion)
            {
                case MigrationVersions.Version16:
                    if (schemaVersion == SchemaVersions.SecurityVersion15 ||
                       schemaVersion == SchemaVersions.AuditTrailSchemaVersion16)
                        return false;
                    break;
            }
            return false;
        }

        private string GetSecurityDatabaseConn(Version schemaVersion)
        {
            if (schemaVersion == SchemaVersions.SecurityVersion15)
                return ConfigurationManager.AppSettings[ConstNames.SecurityConnVer15];

            if (schemaVersion == SchemaVersions.SecurityVersion16)
                return ConfigurationManager.AppSettings[ConstNames.SecurityConnVer16];

            throw new ArgumentException(nameof(schemaVersion));
        }

         
    }
}
