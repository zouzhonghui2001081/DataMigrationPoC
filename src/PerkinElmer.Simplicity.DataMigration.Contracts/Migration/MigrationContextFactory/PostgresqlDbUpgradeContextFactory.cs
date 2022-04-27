using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContextFactory
{
    public class PostgresqlDbUpgradeContextFactory : ContextFactocyBase
    {
        private readonly ReleaseVersions _toVersion;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public PostgresqlDbUpgradeContextFactory(ReleaseVersions toVersion, CancellationTokenSource cancellationTokenSource)
        {
            _toVersion = toVersion;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public override MigrationContextBase GetMigrationContext()
        {
            var sourceContext = GeneratePostgresqlSourceContext();
            var targetContext = GeneratePostgresqlTargetContext();
            var transformContext = GeneratePostgresqlTransformContext();
            var migrationContext = new PostgresqlDbUpgradeMigrationContext
            {
                MigrationDataType = MigrationDataTypes.Project,
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
            var fromVersion = DatabaseUtil.GetChromatographyDatabaseVersion();
            var chromatographyConnName = ConfigurationManager.AppSettings["ChromatographyConn"];
            var auditTrailConnName = ConfigurationManager.AppSettings["AuditTrailConn"];

            var releaseVersion = ReleaseVersions.Unknown;
            if (fromVersion == SchemaVersions.ChromatographySchemaVersion15)
                releaseVersion = ReleaseVersions.Version15;
            if (fromVersion == SchemaVersions.ChromatographySchemaVersion16)
                releaseVersion = ReleaseVersions.Version16;

            if (releaseVersion == ReleaseVersions.Unknown)
                throw new ArgumentException("Failed to get release version from CDS application database! ");

            return new PostgresqlSourceContext
            {
                BlockOption = blockOption,
                FromReleaseVersion = releaseVersion,
                ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnName].ConnectionString,
                AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnName].ConnectionString
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
                case ReleaseVersions.Version15:
                    var chromatographyConnNameV15 = ConfigurationManager.AppSettings["ChromatographyConnVer15"];
                    var auditTrailConnNameV15 = ConfigurationManager.AppSettings["AuditTrailConnVer15"];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        TargetReleaseVersion = _toVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV15].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV15].ConnectionString
                    };
                case ReleaseVersions.Version16:
                    var chromatographyConnNameV16 = ConfigurationManager.AppSettings["ChromatographyConnVer16"];
                    var auditTrailConnNameV16 = ConfigurationManager.AppSettings["AuditTrailConnVer16"];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        TargetReleaseVersion = _toVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV16].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV16].ConnectionString
                    };
            }

            throw new ArgumentException("Target Version not supported!");
        }

        private PostgresqlTransformContext GeneratePostgresqlTransformContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4 ,
                CancellationToken = _cancellationTokenSource.Token
            };
            return new PostgresqlTransformContext
            {
                BlockOption = blockOption
            };
        }
    }
}
