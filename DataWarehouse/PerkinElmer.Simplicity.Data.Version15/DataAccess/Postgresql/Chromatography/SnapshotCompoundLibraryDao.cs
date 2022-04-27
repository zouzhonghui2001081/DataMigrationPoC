using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class SnapshotCompoundLibraryDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "SnapshotCompoundLibrary";
		internal static string IdColumn { get; } = "Id";
		internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
		internal static string LibraryNameColumn { get; } = "LibraryName";
        internal static string LibraryGuidColumn { get; } = "LibraryGuid";
        internal static string CreatedDateColumn { get; } = "CreatedDate";
        internal static string ModifiedDateColumn { get; } = "ModifiedDate";

		protected readonly string SqlInsert =
	        $"INSERT INTO {TableName} " +
	        $"({AnalysisResultSetIdColumn}," +
	        $"{LibraryNameColumn}," +
	        $"{LibraryGuidColumn}," +
	        $"{CreatedDateColumn}," +
			$"{ModifiedDateColumn}) " +
	        "VALUES " +
	        $"(@{AnalysisResultSetIdColumn}," +
	        $"@{LibraryNameColumn}," +
	        $"@{LibraryGuidColumn}," +
	        $"@{CreatedDateColumn}," +
			$"@{ModifiedDateColumn}) ";

        protected readonly string SqlSelect =
	        "SELECT " +
	        $"{TableName}.{IdColumn}," +
	        $"{TableName}.{AnalysisResultSetIdColumn}," +
	        $"{TableName}.{LibraryNameColumn}," +
	        $"{TableName}.{LibraryGuidColumn}," +
	        $"{TableName}.{CreatedDateColumn}," +
			$"{TableName}.{ModifiedDateColumn} ";

		public virtual bool Create(IDbConnection connection, SnapshotCompoundLibrary snapshotCompoundLibrary)
		{
			try
			{
				snapshotCompoundLibrary.Id = connection.ExecuteScalar<long>(SqlInsert + " RETURNING Id", snapshotCompoundLibrary);

				return (snapshotCompoundLibrary.Id != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}
		public List<SnapshotCompoundLibrary> GetAllLibraries(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
		{
			try
			{
				var snapshotCompoundLibraries = connection.Query<SnapshotCompoundLibrary>(
						SqlSelect +
						$"FROM {ProjectDao.TableName} " +
						$"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
						$"INNER JOIN {TableName} ON {TableName}.{AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
						$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
						$"{AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.GuidColumn} = @AnalysisResultSetGuid",
						new { ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid}).ToList();

				return snapshotCompoundLibraries;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAllLibraries method", ex);
				throw;
			}
		}
		public bool IsExists(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid, Guid libraryGuid)
		{
			try
			{
				var snapshotCompoundLibraries = connection.QueryFirstOrDefault<SnapshotCompoundLibrary>(
					SqlSelect +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"INNER JOIN {TableName} ON {TableName}.{AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.GuidColumn} = @AnalysisResultSetGuid AND {TableName}.{LibraryGuidColumn} = @LibraryGuid",
					new { ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid, LibraryGuid= libraryGuid });

				return snapshotCompoundLibraries != null;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAllLibraries method", ex);
				throw;
			}
		}
		public List<SnapshotCompoundLibrary> GetAllLibraries(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var snapshotCompoundLibraries = connection.Query<SnapshotCompoundLibrary>(
					SqlSelect +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"INNER JOIN {TableName} ON {TableName}.{AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid }).ToList();

				return snapshotCompoundLibraries;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAllLibraries method", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, long snapshotCompoundLibraryId, Guid libraryGuid)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {IdColumn} = @SnapshotCompoundLibraryId AND {LibraryGuidColumn} = @LibraryGuid",
					new { SnapshotCompoundLibraryId = snapshotCompoundLibraryId, LibraryGuid = libraryGuid});
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
	}
}
