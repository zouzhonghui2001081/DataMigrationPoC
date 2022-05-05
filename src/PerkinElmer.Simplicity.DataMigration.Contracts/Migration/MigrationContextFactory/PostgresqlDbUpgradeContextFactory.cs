﻿using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;
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
    public class PostgresqlDbUpgradeContextFactory : ContextFactocyBase
    {
        private readonly MigrationVersion _toVersion;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly MigrationControllerBase _controller;

        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PostgresqlDbUpgradeContextFactory(MigrationVersion toVersion, CancellationTokenSource cancellationTokenSource, MigrationControllerBase controller)
        {
            _toVersion = toVersion;
            _cancellationTokenSource = cancellationTokenSource;
            _controller = controller;
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
            var fromVersion = GetChromatographyDatabaseVersion();
            var chromatographyConnName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConn];
            var auditTrailConnName = ConfigurationManager.AppSettings[ConstNames.AuditTrailConn];
            var securityConnName = ConfigurationManager.AppSettings[ConstNames.SecurityConn];

            var migrationVersion = MigrationVersion.Unknown;
            foreach(var sourceHost in _controller.MigrationSourceHost)
            {
                if(sourceHost.Value is PostgresqlSourceHost postgresqlSourceHost)
                {
                    if (fromVersion == postgresqlSourceHost.ChromatographySchemaVersion)
                    {
                        migrationVersion = sourceHost.Key;
                        break;
                    }
                }
            }

            if (migrationVersion == MigrationVersion.Unknown)
                throw new ArgumentException("Failed to get release version from CDS application database! ");

            return new PostgresqlSourceContext
            {
                BlockOption = blockOption,
                MigrateFromVersion = migrationVersion,
                SourceParamType = Source.SourceParamTypes.ProjectGuid,
                ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnName].ConnectionString,
                AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnName].ConnectionString,
                SecurityConnection = ConfigurationManager.ConnectionStrings[securityConnName].ConnectionString,
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
                case MigrationVersion.Version15:
                    var chromatographyConnNameV15 = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer15];
                    var auditTrailConnNameV15 = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer15];
                    var securityConnNameV15 = ConfigurationManager.AppSettings[ConstNames.SecurityConnVer15];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        MigrateToVersion = _toVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV15].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV15].ConnectionString,
                        SecurityConnection = ConfigurationManager.ConnectionStrings[securityConnNameV15].ConnectionString
                    };
                case MigrationVersion.Version16:
                    var chromatographyConnNameV16 = ConfigurationManager.AppSettings[ConstNames.ChromatographyConnVer16];
                    var auditTrailConnNameV16 = ConfigurationManager.AppSettings[ConstNames.AuditTrailConnVer16];
                    var securityConnNameV16 = ConfigurationManager.AppSettings[ConstNames.SecurityConnVer16];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        MigrateToVersion = _toVersion,
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

        private Version GetChromatographyDatabaseVersion()
        {
            try
            {
                var connectionStringName = ConfigurationManager.AppSettings[ConstNames.PostgresqlDefaultDb];
                var defaultConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                var defaultDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(defaultConnectionString);

                var chromatographyConnName = ConfigurationManager.AppSettings[ConstNames.ChromatographyConn];
                var chromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnName].ConnectionString;
                var appDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(chromatographyConnection);

                using (var connection = new NpgsqlConnection(defaultDbConnectionStrBuilder.ConnectionString))
                {
                    connection.Open();

                    var chromatographyDatabaseExists = connection.ExecuteScalar<bool>(
                        $"SELECT EXISTS(SELECT 1 FROM pg_catalog.pg_database where datname = '{appDbConnectionStrBuilder.Database}');");
                    connection.Close();

                    if (chromatographyDatabaseExists == false)
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
    }
}
