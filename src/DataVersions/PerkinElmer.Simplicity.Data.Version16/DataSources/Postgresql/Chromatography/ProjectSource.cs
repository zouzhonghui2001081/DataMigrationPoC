using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class ProjectSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version16DataBase> GetProjects(IList<Guid> projectGuids, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
            var projectDao = new ProjectDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var projectGuid in projectGuids)
                {
                    var project = projectDao.GetProject(connection, projectGuid);
                    var projectData = new ProjectData { Project = project };
                    //TODO : Audit Trail logs logic for project
                    if (project != null) migrationEntities.Add(projectData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version16DataBase> GetAllProjects(PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
            var projectDao = new ProjectDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var projects = projectDao.GetAllProjects(connection);
                foreach (var project in projects)
                {
                    var projectData = new ProjectData { Project = project };
                    migrationEntities.Add(projectData);
                }
                connection.Close();
            }

            return migrationEntities;
        }
    }
}
