using System;
using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql
{
    public class PostgresqlSourceHostVer15 : PostgresqlSourceHost
    {
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
