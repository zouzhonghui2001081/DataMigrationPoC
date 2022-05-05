using System;
using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql
{
    public class PostgresqlSourceHostVer16 : PostgresqlSourceHost
    {
        public override Version AuditTrailSchemaVersion => new Version(0, 6);

        public override Version SecuritySchemaVersion => new Version(1, 8);

        public override Version ChromatographySchemaVersion => new Version(1, 10);

        public override int ChromatographyMajorDataVersion => -1;

        public override int ChromatographyMinorDataVersion => 33;

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
    }
}
