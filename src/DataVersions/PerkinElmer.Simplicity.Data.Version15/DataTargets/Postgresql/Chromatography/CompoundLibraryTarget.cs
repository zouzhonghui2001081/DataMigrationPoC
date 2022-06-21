using System;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    internal class CompoundLibraryTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveCompoundLibrary(CompoundLibraryData compoundLibraryData, PostgresqlTargetContext postgresqlTargetContext)
        {
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();
            var projectDao = new ProjectDao();
            var projectCompoundLibraryDao = new ProjectCompoundLibraryDao();
            var compoundLibraryItemDao = new CompoundLibraryItemDao();
            var projectCompoundLibraryToLibraryItemMapDao = new ProjectCompoundLibraryToLibraryItemMapDao();

            var project = projectDao.GetProject(connection, compoundLibraryData.ProjectGuid);
            if (project == null) throw new ArgumentNullException(nameof(project));
            if (projectCompoundLibraryDao.GetCompoundLibraryByLibraryGuid(connection, project.Guid, compoundLibraryData.ProjectCompoundLibrary.LibraryGuid) != null)
                return;

            compoundLibraryData.ProjectCompoundLibrary.ProjectId = project.Id;

            projectCompoundLibraryDao.Create(connection, compoundLibraryData.ProjectCompoundLibrary);
            compoundLibraryItemDao.Create(connection, compoundLibraryData.ProjectCompoundLibrary.CompoundLibraryItems);


            var compoundLibraryDao = new ProjectCompoundLibraryDao();
            var compoundLibrary = compoundLibraryDao.GetCompoundLibraryByLibraryGuid(connection, project.Guid, compoundLibraryData.ProjectCompoundLibrary.LibraryGuid);

            foreach (var compoundLibraryItem in compoundLibraryData.ProjectCompoundLibrary.CompoundLibraryItems)
            {
                var map = new ProjectCompoundLibraryToLibraryItemMap
                {
                    CompoundLibraryItemId = compoundLibraryItem.Id,
                    ProjectCompoundLibraryId = compoundLibrary.Id
                };
                projectCompoundLibraryToLibraryItemMapDao.Create(connection, map);
            }

            connection.Close();
        }
	}
}
