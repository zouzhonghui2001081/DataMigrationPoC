using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Data;
using PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class CompoundLibrarySource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version16DataBase> GetCompoundLibrary(Guid projectGuid)
        {
            var migrationEntities = new List<Version16DataBase>();
            var compoundLibraryDao = new ProjectCompoundLibraryDao();
            var compoundLibraryItemDao = new CompoundLibraryItemDao();

            using (var connection = new NpgsqlConnection(Version16Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var compounds = compoundLibraryDao.GetAllLibraries(connection, projectGuid);
                foreach (var compound in compounds)
                {
                    var compoundLibraryItems = compoundLibraryItemDao.GetProjectCompoundLibraryItems(connection, projectGuid, compound.LibraryGuid);
                    compound.CompoundLibraryItems = compoundLibraryItems;
                    var compoundLibraryData = new CompoundLibraryData
                    {
                        ProjectGuid = projectGuid,
                        ProjectCompoundLibrary = compound
                    };
                    migrationEntities.Add(compoundLibraryData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version16DataBase> GetCompoundLibrary(Guid projectGuid, IList<Guid> compoundLibraryGuids)
        {
            var migrationEntities = new List<Version16DataBase>();
            var compoundLibraryDao = new ProjectCompoundLibraryDao();
            var compoundLibraryItemDao = new CompoundLibraryItemDao();

            using (var connection = new NpgsqlConnection(Version16Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var compoundLibraryGuid in compoundLibraryGuids)
                {
                    var compound = compoundLibraryDao.GetCompoundLibraryByLibraryGuid(connection, projectGuid, compoundLibraryGuid);
                    var compoundLibraryItems = compoundLibraryItemDao.GetProjectCompoundLibraryItems(connection, projectGuid, compound.LibraryGuid);
                    compound.CompoundLibraryItems = compoundLibraryItems;
                    var compoundLibraryData = new CompoundLibraryData
                    {
                        ProjectGuid = projectGuid,
                        ProjectCompoundLibrary = compound
                    };
                    migrationEntities.Add(compoundLibraryData);
                }
                connection.Close();
            }

            return migrationEntities;
        }
        
        internal static IList<SnapshotCompoundLibraryData> CreateCompoundLibraryData(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
        {
            var compoundLibraryData = new List<SnapshotCompoundLibraryData>();
            var snapshotCompoundLibraryDao = new SnapshotCompoundLibraryDao();
            var compoundLibraryItemDao = new CompoundLibraryItemDao();

            var snapshotCompoundLibraries = snapshotCompoundLibraryDao.GetAllLibraries(connection, projectGuid, analysisResultSetGuid);
            foreach (var snapshotCompoundLibrary in snapshotCompoundLibraries)
            {
                var compoundLibraryItems = compoundLibraryItemDao.GetAnalysisResultSetCompoundLibraryItems(connection, projectGuid, analysisResultSetGuid, snapshotCompoundLibrary.LibraryGuid);
                compoundLibraryData.Add(new SnapshotCompoundLibraryData
                {
                    SnapshotCompoundLibrary = snapshotCompoundLibrary,
                    CompoundLibraryItems = compoundLibraryItems
                });
            }

            return compoundLibraryData;
        }
    }
}
