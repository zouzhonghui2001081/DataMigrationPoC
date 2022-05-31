using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class NamedContentDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "NamedContent";
		internal static string IdColumn { get; } = "Id";
		internal static string BatchRunIdColumn { get; } = "BatchRunId";
		internal static string KeyColumn { get; } = "Key";
		internal static string ValueColumn { get; } = "Value";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			$"({BatchRunIdColumn}," +
			$"{KeyColumn}," +
			$"{ValueColumn}) " +
			"VALUES " +
			$"(@{BatchRunIdColumn}," +
			$"@{KeyColumn}," +
			$"@{ValueColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {TableName}.{IdColumn}," +
			$"{TableName}.{BatchRunIdColumn}," +
			$"{TableName}.{KeyColumn}," +
			$"{TableName}.{ValueColumn} ";

		public NamedContent[] Get(IDbConnection connection, string projectName, string batchResultName, Guid batchRunGuid)
		{
			try
			{
				projectName = projectName.Replace("'", "''").ToLower();
				batchResultName = batchResultName.Replace("'", "''").ToLower();

				var select = SelectSql +
				            $"FROM {ProjectDao.TableName} " +
				            $"INNER JOIN {BatchResultSetDao.TableName} ON {BatchResultSetDao.TableName}.{BatchResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				            $"INNER JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.BatchResultSetIdColumn} = {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} " +
				            $"INNER JOIN {TableName} ON {TableName}.{BatchRunIdColumn} = {BatchRunDao.TableName}.{BatchRunDao.IdColumn} " +
				            "WHERE " +
				            $"LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND " +
				            $"LOWER({BatchResultSetDao.TableName}.{BatchResultSetDao.NameColumn}) = @BatchResultName AND " +
				            $"{BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = @BatchRunGuid";

				return connection.Query<NamedContent>(select, 
					new {ProjectName = projectName,
						BatchResultName = batchResultName,
						BatchRunGuid = batchRunGuid }).ToArray();
			}
			catch (Exception ex)
			{
				Log.Error("Error in Get method", ex);
				throw;
			}
		}
		public NamedContent[] Get(IDbConnection connection, string projectName, Guid batchResultGuid, Guid batchRunGuid)
		{
			try
			{
				projectName = projectName.Replace("'", "''").ToLower();

				var select = SelectSql +
				             $"FROM {ProjectDao.TableName} " +
				             $"INNER JOIN {BatchResultSetDao.TableName} ON {BatchResultSetDao.TableName}.{BatchResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				             $"INNER JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.BatchResultSetIdColumn} = {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} " +
				             $"INNER JOIN {TableName} ON {TableName}.{BatchRunIdColumn} = {BatchRunDao.TableName}.{BatchRunDao.IdColumn} " +
				             "WHERE " +
				             $"LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND " +
				             $"{BatchResultSetDao.TableName}.{BatchResultSetDao.GuidColumn} = @BatchResultGuid AND " +
				             $"{BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = @BatchRunGuid";

				return connection.Query<NamedContent>(select,
					new
					{
						ProjectName = projectName,
						BatchResultGuid = batchResultGuid,
						BatchRunGuid = batchRunGuid
					}).ToArray();
			}
			catch (Exception ex)
			{
				Log.Error("Error in Get method", ex);
				throw;
			}
		}
		public NamedContent[] Get(IDbConnection connection, long batchRunId)
		{
			try
			{
				return connection.Query<NamedContent>(
					SelectSql + 
					$"FROM {TableName} " + 
					$"WHERE {BatchRunIdColumn} = @{BatchRunIdColumn}",
					new
					{
						BatchRunId = batchRunId
					}).ToArray();
			}
			catch (Exception ex)
			{
				Log.Error("Error in Get method", ex);
				throw;
			}
		}
		public virtual void Create(IDbConnection connection, NamedContent[] namedContents)
		{
			try
			{
				foreach (var namedContent in namedContents)
				{
					namedContent.Id = connection.ExecuteScalar<long>(InsertSql + "Returning Id", namedContent);
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, long batchRunId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {BatchRunIdColumn} = @{BatchRunIdColumn}", 
					new {BatchRunId = batchRunId}
				);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}
	}
}
