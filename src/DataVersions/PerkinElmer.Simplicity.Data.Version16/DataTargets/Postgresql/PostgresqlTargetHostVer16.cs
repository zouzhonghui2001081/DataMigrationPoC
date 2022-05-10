using System;
using System.IO;
using System.Text.Json;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;
using SchemaVersions = PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SchemaVersions;
using DataVersions = PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.DataVersions;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql
{
    public class PostgresqlTargetHostVer16 : PostgresqlTargetHost
    {
        public override Version AuditTrailSchemaVersion => SchemaVersions.AuditTrailSchemaVersion;

        protected override string AuditTrailDbSchema => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.AuditTrailDBSchema.sql";

        public override Version SecuritySchemaVersion => SchemaVersions.SecurityVersion;

        protected override string SecurityDbSchema => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.SecurityDbSchema.sql";

        protected override string SecurityData => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.SecurityDbData.sql";

        public override Version ChromatographySchemaVersion => SchemaVersions.ChromatographySchemaVersion;

        public override int ChromatographyMajorDataVersion => DataVersions.ChromatographyDataVersionMajor;

        public override int ChromatographyMinorDataVersion => DataVersions.ChromatographyDataVersionMinor;

        protected override string ChromatographyDbSchema => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.ChromatographyDBSchema.sql";

        protected override string ChromatographyNotificationFunctionTriggers => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.Version15.NotificationFunctionTriggers.sql";

        protected override string ChromatographyDummyData => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.SQL.DummyRuns.sql";

        protected override string ConnectionStringResourceName => "PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.ConnectionStrings.json";

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

        protected override ConnectionStrings GetConnectionStrings()
        {
            var assembly = typeof(PostgresqlTargetHostVer16).Assembly;

            using (var stream = assembly.GetManifestResourceStream(ConnectionStringResourceName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                         $"Failed to load resource {ConnectionStringResourceName}")))
                {
                    var strings = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<ConnectionStrings>(strings);
                }
            }
        }
    }
}
