using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks.Dataflow;

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
            var migrationContext = new MigrationContext(MigrationType.Upgrade)
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
            var auditTrailSchemaVersion = GetSchemaVersion(PostgresqlDatabases.AuditTrail);
            var securitySchemaVersion = GetSchemaVersion(PostgresqlDatabases.Security);

            return new PostgresqlSourceContext
            {
                BlockOption = blockOption,
                MigrateFromVersion = GetMigrationVersion(chromatographySchemaVersion),
                SourceParamType = Source.SourceParamTypes.ProjectGuid,
                ChromatographyConnection = GetDefaultConnectionString(PostgresqlDatabases.Chromatography),
                IsMigrateAuditTrail = IsNeedMigrateAuditTrail(auditTrailSchemaVersion),
                AuditTrailConnection = GetDefaultConnectionString(PostgresqlDatabases.AuditTrail),
                IsMigrateSecurity = IsNeedMigrateSecurity(securitySchemaVersion),
                SecurityConnection = GetDefaultConnectionString(PostgresqlDatabases.Security),
            };
        }

        private PostgresqlTargetContext GeneratePostgresqlTargetContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = _cancellationTokenSource.Token
            };

            var auditTrailSchemaVersion = GetSchemaVersion(PostgresqlDatabases.AuditTrail);
            var securitySchemaVersion = GetSchemaVersion(PostgresqlDatabases.Security);
            return new PostgresqlTargetContext
            {
                BlockOption = blockOption,
                MigrateToVersion = _toVersion,
                ChromatographyConnection = GetTargetChromatographyDatabaseConn(),
                IsMigrateAuditTrail = IsNeedMigrateAuditTrail(auditTrailSchemaVersion),
                AuditTrailConnection = GetTargetAuditTrailDatabaseConn(),
                IsMigrateSecurity = IsNeedMigrateSecurity(securitySchemaVersion),
                SecurityConnection = GetTargetSecurityDatabaseConn()
            };

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

        private Version GetSchemaVersion(PostgresqlDatabases database)
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

        private string GetDefaultConnectionString(PostgresqlDatabases database)
        {
            switch (database)
            {
                case PostgresqlDatabases.Chromatography:
                    var chromatographyConnName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConn];
                    return ConfigurationManager.ConnectionStrings[chromatographyConnName].ConnectionString;
                case PostgresqlDatabases.AuditTrail:
                    var auditTrailConnName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConn];
                    return ConfigurationManager.ConnectionStrings[auditTrailConnName].ConnectionString;
                case PostgresqlDatabases.Security:
                    var securityConnName = ConfigurationManager.AppSettings[ConstNames.SecurityConn];
                    return ConfigurationManager.ConnectionStrings[securityConnName].ConnectionString;
            }

            throw new NotImplementedException();
        }

        private MigrationVersions GetMigrationVersion(Version chromatographySchemaVersion)
        {
            if (chromatographySchemaVersion == SchemaVersions.ChromatographySchemaVersion15)
                return MigrationVersions.Version15;

            if (chromatographySchemaVersion == SchemaVersions.ChromatographySchemaVersion16)
                return MigrationVersions.Version16;
            throw new NotImplementedException();
        }

        private string GetTargetChromatographyDatabaseConn()
        {
            if (_toVersion == MigrationVersions.Version15)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer15];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            if (_toVersion == MigrationVersions.Version16)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer16];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            throw new NotImplementedException();
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

        private string GetTargetAuditTrailDatabaseConn()
        {
            if (_toVersion == MigrationVersions.Version15)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer15];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            if (_toVersion == MigrationVersions.Version16)
            {
                var connectName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer16];
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }

            throw new NotImplementedException();
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

        private string GetTargetSecurityDatabaseConn()
        {
            if (_toVersion == MigrationVersions.Version15)
                return ConfigurationManager.AppSettings[ConstNames.SecurityConnVer15];

            if (_toVersion == MigrationVersions.Version16)
                return ConfigurationManager.AppSettings[ConstNames.SecurityConnVer16];

            throw new NotImplementedException();
        }
    }
}
