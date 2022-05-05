using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;
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
        private readonly MigrationVersion _fromVersion;
        private readonly MigrationVersion _toVersion;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly MigrationControllerBase _controller;

        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PostgresqlDbUpgradeContextFactory(MigrationVersion fromVersion, MigrationVersion toVersion, CancellationTokenSource cancellationTokenSource, MigrationControllerBase controller)
        {
            _fromVersion = fromVersion;
            _toVersion = toVersion;
            _cancellationTokenSource = cancellationTokenSource;
            _controller = controller;
            if(controller?.MigrationType != MigrationType.Upgrade)
            {
                throw new ArgumentException("Invalid migration controller passed into the factory.", nameof(controller));
            }
        }

        public override MigrationContext GetMigrationContext()
        {
            var sourceContext = GeneratePostgresqlSourceContext();
            var targetContext = GeneratePostgresqlTargetContext();
            targetContext.IsMigrateAuditTrail = sourceContext.IsMigrateAuditTrail;
            targetContext.IsMigrateSecurity = sourceContext.IsMigrateSecurity;
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

            var sourceHost = _controller.MigrationSourceHost[_fromVersion] as PostgresqlSourceHost;
            var auditTrailSchemaVersion = GetSchemaVersion(sourceHost.ConnectionStrings.AuditTrail);
            var securitySchemaVersion = GetSchemaVersion(sourceHost.ConnectionStrings.Security);

            return new PostgresqlSourceContext
            {
                BlockOption = blockOption,
                MigrateFromVersion = _fromVersion,
                SourceParamType = Source.SourceParamTypes.ProjectGuid,
                ChromatographyConnection = sourceHost.ConnectionStrings.Chromatography,
                IsMigrateAuditTrail = NeedMigrateAuditTrail(auditTrailSchemaVersion),
                AuditTrailConnection = sourceHost.ConnectionStrings.AuditTrail,
                IsMigrateSecurity = NeedMigrateSecurity(securitySchemaVersion),
                SecurityConnection = sourceHost.ConnectionStrings.Security,
            };
        }

        private PostgresqlTargetContext GeneratePostgresqlTargetContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = _cancellationTokenSource.Token
            };

            var targetHost = _controller.MigrationTargetHost[_fromVersion] as PostgresqlTargetHost;
            return new PostgresqlTargetContext
            {
                BlockOption = blockOption,
                MigrateToVersion = _toVersion,
                ChromatographyConnection = targetHost.ConnectionStrings.Chromatography,
                AuditTrailConnection = targetHost.ConnectionStrings.AuditTrail,
                SecurityConnection = targetHost.ConnectionStrings.Security
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

        private Version GetSchemaVersion(string connectionString)
        {
            try
            {
                var appDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(connectionString);

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

        private bool NeedMigrateAuditTrail(Version schemaVersion)
        {
            var toVersionTargetHost = _controller.MigrationTargetHost[_toVersion];
            if(toVersionTargetHost is PostgresqlTargetHost postgresqlTargetHost)
            {
                if(postgresqlTargetHost.AuditTrailSchemaVersion > schemaVersion)
                {
                    return true;
                }
            }

            return false;
        }

        private bool NeedMigrateSecurity(Version schemaVersion)
        {
            var toVersionTargetHost = _controller.MigrationTargetHost[_toVersion];
            if (toVersionTargetHost is PostgresqlTargetHost postgresqlTargetHost)
            {
                if (postgresqlTargetHost.SecuritySchemaVersion > schemaVersion)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
