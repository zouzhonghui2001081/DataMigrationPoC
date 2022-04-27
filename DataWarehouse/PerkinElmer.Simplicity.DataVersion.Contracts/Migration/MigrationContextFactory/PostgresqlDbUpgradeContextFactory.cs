using System;
using System.Configuration;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Common.Postgresql;
using PerkinElmer.Simplicity.Data.Common.Postgresql.Utils;
using PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.Data.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Contracts.Transform.TransformContext;

namespace PerkinElmer.Simplicity.Data.Contracts.Migration.MigrationContextFactory
{
    public class PostgresqlDbUpgradeContextFactory : ContextFactocyBase
    {
        private readonly ReleaseVersions _toVersion;

        public PostgresqlDbUpgradeContextFactory(ReleaseVersions toVersion)
        {
            _toVersion = toVersion;
        }

        public override MigrationContextBase GetMigrationContext()
        {
            var sourceContext = GeneratePostgresqlSourceContext();
            var targetContext = GeneratePostgresqlTargetContext(_toVersion);
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
            var blockOption = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4 };
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

        private PostgresqlTargetContext GeneratePostgresqlTargetContext(ReleaseVersions targetReleaseVersion)
        {
            var blockOption = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4 };
            switch (targetReleaseVersion)
            {
                case ReleaseVersions.Version15:
                    var chromatographyConnNameV15 = ConfigurationManager.AppSettings["ChromatographyConnVer15"];
                    var auditTrailConnNameV15 = ConfigurationManager.AppSettings["AuditTrailConnVer15"];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        TargetReleaseVersion = targetReleaseVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV15].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV15].ConnectionString
                    };
                case ReleaseVersions.Version16:
                    var chromatographyConnNameV16 = ConfigurationManager.AppSettings["ChromatographyConnVer16"];
                    var auditTrailConnNameV16 = ConfigurationManager.AppSettings["AuditTrailConnVer16"];
                    return new PostgresqlTargetContext
                    {
                        BlockOption = blockOption,
                        TargetReleaseVersion = targetReleaseVersion,
                        ChromatographyConnection = ConfigurationManager.ConnectionStrings[chromatographyConnNameV16].ConnectionString,
                        AuditTrailConnection = ConfigurationManager.ConnectionStrings[auditTrailConnNameV16].ConnectionString
                    };
            }

            throw new ArgumentException(nameof(targetReleaseVersion));
        }

        private PostgresqlTransformContext GeneratePostgresqlTransformContext()
        {
            var blockOption = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4 };
            return new PostgresqlTransformContext
            {
                BlockOption = blockOption
            };
        }
    }
}
