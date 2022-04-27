using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Utils;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class ProjectCompoundLibraryDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "ProjectCompoundLibrary";
		internal static string IdColumn { get; } = "Id";
		internal static string ProjectIdColumn { get; } = "ProjectId";
		internal static string CreatedDateColumn { get; } = "CreatedDate";
		internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
		internal static string CreatedUserNameColumn { get; } = "CreatedUserName";
		internal static string ModifiedDateColumn { get; } = "ModifiedDate";
		internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
		internal static string ModifiedUserNameColumn { get; } = "ModifiedUserName";
		internal static string LibraryNameColumn { get; } = "LibraryName";
        internal static string LibraryGuidColumn { get; } = "LibraryGuid";
		internal static string DescriptionColumn { get; } = "Description";

		protected readonly string SqlInsert =
			$"INSERT INTO {TableName} " +
			$"({ProjectIdColumn}," +
			$"{CreatedDateColumn}," +
			$"{CreatedUserIdColumn}," +
			$"{CreatedUserNameColumn}," +
			$"{ModifiedDateColumn}," +
			$"{ModifiedUserIdColumn}," +
			$"{ModifiedUserNameColumn}," +
			$"{LibraryNameColumn}," +
            $"{LibraryGuidColumn}," +
			$"{DescriptionColumn}) " +
			"VALUES " +
			$"(@{ProjectIdColumn}," +
			$"@{CreatedDateColumn}," +
			$"@{CreatedUserIdColumn}," +
			$"@{CreatedUserNameColumn}," +
			$"@{ModifiedDateColumn}," +
			$"@{ModifiedUserIdColumn}," +
			$"@{ModifiedUserNameColumn}," +
			$"@{LibraryNameColumn}," +
            $"@{LibraryGuidColumn}," +
			$"@{DescriptionColumn}) ";

		protected readonly string SqlSelect =
			$"SELECT " +
			$"{TableName}.{IdColumn}," +
			$"{TableName}.{ProjectIdColumn}," +
			$"{TableName}.{CreatedDateColumn}," +
			$"{TableName}.{CreatedUserIdColumn}," +
			$"{TableName}.{CreatedUserNameColumn}," +
			$"{TableName}.{ModifiedDateColumn}," +
			$"{TableName}.{ModifiedUserIdColumn}," +
			$"{TableName}.{ModifiedUserNameColumn}," +
			$"{TableName}.{LibraryNameColumn}," +
            $"{TableName}.{LibraryGuidColumn}," +
			$"{TableName}.{DescriptionColumn} " +
			$"FROM {TableName} ";

		private readonly string _sqlUpdate =
			$"UPDATE {TableName} " +
			"SET " +
			$"{ModifiedDateColumn}=@{ModifiedDateColumn}," +
			$"{ModifiedUserIdColumn}=@{ModifiedUserIdColumn}," +
			$"{DescriptionColumn}=@{DescriptionColumn} ";


		public virtual bool Create(IDbConnection connection, ProjectCompoundLibrary compoundLibrary)
		{
			try
			{
				var recordsAdded = connection.Execute(SqlInsert, compoundLibrary);

				return (recordsAdded != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}
		public List<ProjectCompoundLibrary> GetAllLibraries(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var compoundLibraries = connection.Query<ProjectCompoundLibrary>(
						SqlSelect +
						$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
						$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
                        new { ProjectGuid = projectGuid}).ToQuickList(0);

				return compoundLibraries;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAllLibraries method", ex);
				throw;
			}
		}
        public List<string> GetAllCompoundLibraryNames(IDbConnection connection, Guid projectGuid)
		{
            try
            {
                var compoundLibraries = connection.Query<string>(
                        $"SELECT {TableName}.{LibraryNameColumn} " +
                        $"FROM {TableName} " +
                        $"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
                        $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
                        new { ProjectGuid = projectGuid}).ToList();
                
                return compoundLibraries;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetAllCompoundLibraryNames method", ex);
                throw ;
            }
		}
		public List<ProjectCompoundLibrary> GetCompoundLibrariesByProjectName(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var compoundLibraries = connection.Query<ProjectCompoundLibrary>(
						SqlSelect +
						$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
						$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
                        new { ProjectGuid = projectGuid}).ToList();
				return compoundLibraries;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetCompoundLibrariesByProjectName method", ex);
				throw;
			}
		}

		public ProjectCompoundLibrary GetCompoundLibraryByLibraryName(IDbConnection connection, Guid projectGuid, string libraryName)
		{
			try
			{
				var compoundLibrary = connection.QueryFirstOrDefault<ProjectCompoundLibrary>(
					SqlSelect +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
					$"WHERE " +
					$"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = '{projectGuid}' AND " +
					$"LOWER({TableName}.{LibraryNameColumn}) = LOWER(@LibraryName)", new { LibraryName = libraryName });

				return compoundLibrary;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetCompoundLibraryByLibraryName method", ex);
				throw;
			}
		}

		public ProjectCompoundLibrary GetCompoundLibraryByLibraryGuid(IDbConnection connection, Guid projectGuid, Guid libraryGuid)
		{
			try
			{
				var compoundLibrary = connection.QueryFirstOrDefault<ProjectCompoundLibrary>(
					SqlSelect +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
					"WHERE " +
					$"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{TableName}.{LibraryGuidColumn} = @LibraryGuid",
					new { ProjectGuid = projectGuid, LibraryGuid = libraryGuid});

				return compoundLibrary;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetCompoundLibraryByLibraryGuid method", ex);
				throw;
			}
		}

		public bool Rename(IDbConnection connection, Guid projectGuid, Guid libraryGuid, string newLibraryName, DateTime modifiedDateTime, string modifiedUserId)
		{
			try
			{
				int recordsDeleted = 0;
				var projectId = ProjectDao.GetProjectId(connection, projectGuid);

				if (projectId.HasValue)
				{
					recordsDeleted = connection.Execute(
						$"UPDATE {TableName} " +
						$"SET {LibraryNameColumn}=@{LibraryNameColumn}, " +
						$"{ModifiedDateColumn}=@{ModifiedDateColumn}," +
						$"{ModifiedUserIdColumn}=@{ModifiedUserIdColumn} " +
						$"WHERE {ProjectIdColumn}={projectId.Value} AND {TableName}.{LibraryGuidColumn} = '{libraryGuid}'",
						new {LibraryName = newLibraryName, ModifiedDate = modifiedDateTime, ModifiedUserId = modifiedUserId});
				}

				return (recordsDeleted != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Rename method", ex);
				throw;
			}
		}

		public bool Update(IDbConnection connection, Guid projectGuid, Guid libraryGuid, string description, string modifiedUserId,
			DateTime modifiedDate)
		{
			try
			{
				int rowsUpdated = 0;
				var projectId = ProjectDao.GetProjectId(connection, projectGuid);

				if (projectId.HasValue)
				{
					rowsUpdated = connection.Execute(
						_sqlUpdate +
						$"WHERE {ProjectIdColumn}={projectId.Value} AND {TableName}.{LibraryGuidColumn} = '{libraryGuid}'",
						new {Description = description,
							ModifiedUserId = modifiedUserId,
							ModifiedDate = modifiedDate});
				}

				return (rowsUpdated != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Update method", ex);
				throw;
			}
		}
		
        public bool UpdateModifiedDateUserId(IDbConnection connection, Guid projectGuid, Guid libraryGuid,
			DateTime modifiedDate, long modifiedUserId)
		{
			try
			{
				int recordsDeleted = 0;
				var projectId = ProjectDao.GetProjectId(connection, projectGuid);

				if (projectId.HasValue)
				{
                    recordsDeleted = connection.Execute(
						$"UPDATE {TableName} " +
						$"SET {ModifiedDateColumn}=@{ModifiedDateColumn}," +
						$"{ModifiedUserIdColumn}=@{ModifiedUserIdColumn} " +
						$"WHERE {ProjectIdColumn}={projectId.Value} AND {TableName}.{LibraryGuidColumn} = '{libraryGuid}'",
						new { ModifiedDate = modifiedDate, ModifiedUserId = modifiedUserId });
				}

				return (recordsDeleted != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in UpdateModifiedDateUserId method", ex);
				throw;
			}
		}

		public bool IsExists(IDbConnection connection, Guid projectGuid, string libraryName)
		{
			try
			{
				var compoundLibrary = GetCompoundLibraryByLibraryName(connection, projectGuid, libraryName);
				return (compoundLibrary != null);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in IsExists method", ex);
				throw;
			}
		}
		
        public bool Delete(IDbConnection connection, Guid projectGuid, Guid libraryGuid)
		{
			try
			{
				int recordsDeleted = 0;
				var projectId = ProjectDao.GetProjectId(connection, projectGuid);

				if (projectId.HasValue)
				{
					recordsDeleted = connection.Execute(
						$"DELETE FROM {TableName} " +
						$"WHERE {ProjectIdColumn}={projectId.Value} AND {TableName}.{LibraryGuidColumn} = '{libraryGuid}'");
				}

				return (recordsDeleted != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
		
        public bool Delete(IDbConnection connection, long compoundLibraryId)
		{
			try
			{
				int recordsDeleted = connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {IdColumn} = @Id",
					new {Id = compoundLibraryId});

				return recordsDeleted != 0;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
	}
}
