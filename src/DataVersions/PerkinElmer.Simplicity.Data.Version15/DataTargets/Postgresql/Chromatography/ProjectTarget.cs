using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    internal class ProjectTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveProject(ProjectData projectData)
        {
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var projectDao = new ProjectDao();
                if (!projectDao.IsExists(connection, projectData.Project.Name))
                    projectDao.CreateProject(connection, projectData.Project);
                connection.Close();
            }
        }
    }
}
