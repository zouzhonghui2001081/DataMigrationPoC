using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql
{
    public class PostgresqlSourceHostVer15 : PostgresqlSourceHost
    {
        public override Version AuditTrailSchemaVersion => SchemaVersions.AuditTrailSchemaVersion;

        public override Version SecuritySchemaVersion => SchemaVersions.SecurityVersion;

        public override Version ChromatographySchemaVersion => SchemaVersions.ChromatographySchemaVersion;

        public override int ChromatographyMajorDataVersion => DataVersions.ChromatographyDataVersionMajor;

        public override int ChromatographyMinorDataVersion => DataVersions.ChromatographyDataVersionMinor;

        protected override string ConnectionStringResourceName => "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.ConnectionStrings.json";

        public override IList<SourceParamBase> GetSourceBlockInputParams(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext postgresqlSourceContext))
                throw new ArgumentException(nameof(sourceContext));
            var projectParams = new List<SourceParamBase>();
            switch (postgresqlSourceContext.SourceParamType)
            {
                case SourceParamTypes.ProjectGuid:
                    using (var dbConnection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnection))
                    {
                        var projectDao = new ProjectDao();
                        var projects = projectDao.GetAllProjects(dbConnection);
                        foreach (var project in projects)
                            projectParams.Add(new ProjectSourceParams(project.Guid));
                    }
                    break;
            }
            return projectParams;

        }

        protected override ConnectionStrings GetConnectionStrings()
        {
            var assembly = typeof(PostgresqlSourceHostVer15).Assembly;

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
