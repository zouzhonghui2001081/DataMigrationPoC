using System;
using System.IO;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql
{
    public class PostgresqlTargetHostVer16 : PostgresqlTargetHost
    {
        protected override Version AuditTrailSchemaVersion => SchemaVersions.AuditTrailSchemaVersion16;

        protected override string AuditTrailDbSchema => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.AuditTrailDBSchema.sql";

        protected override Version SecuritySchemaVersion => SchemaVersions.SecurityVersion16;

        protected override string SecurityDbSchema => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.SecurityDbSchema.sql";

        protected override string SecurityData => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.SecurityDbData.sql";

        protected override Version ChromatographySchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        protected override int ChromatographyMajorDataVersion => DataVersions.ChromatographyDataVersion16Major;

        protected override int ChromatographyMinorDataVersion => DataVersions.ChromatographyDataVersion16Minor;

        protected override string ChromatographyDbSchema => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.ChromatographyDBSchema.sql";

        protected override string ChromatographyNotificationFunctionTriggers => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.Version15.NotificationFunctionTriggers.sql";

        protected override string ChromatographyDummyData => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.DummyRuns.sql";

        protected override string GetSqlScript(string resourceName)
        {
            Log.Info("GetSqlScript() called");

            var assembly = typeof(PostgresqlTargetHostVer16).Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                         $"Failed to load resource {resourceName}")))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
