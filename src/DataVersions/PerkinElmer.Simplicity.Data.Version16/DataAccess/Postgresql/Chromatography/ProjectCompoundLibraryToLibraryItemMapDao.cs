using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class ProjectCompoundLibraryToLibraryItemMapDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "ProjectCompoundLibraryToLibraryItemMap";
		internal static string IdColumn { get; } = "Id";
		internal static string ProjectCompoundLibraryIdColumn { get; } = "ProjectCompoundLibraryId";
		internal static string CompoundLibraryItemIdColumn { get; } = "CompoundLibraryItemId";

		private readonly string _sqlInsert =
			$"INSERT INTO {TableName} " +
			$"({ProjectCompoundLibraryIdColumn}," +
			$"{CompoundLibraryItemIdColumn})" +
			"VALUES " +
			$"(@{ProjectCompoundLibraryIdColumn}," +
			$"@{CompoundLibraryItemIdColumn}) ";

		private readonly string _sqlSelect =
			"SELECT " +
			$"{TableName}.{IdColumn}," +
			$"{TableName}.{ProjectCompoundLibraryIdColumn}," +
			$"{TableName}.{CompoundLibraryItemIdColumn} " +
			$"FROM {TableName} ";

		public bool Create(IDbConnection connection, ProjectCompoundLibraryToLibraryItemMap map)
		{
			var recordsAdded = connection.Execute(_sqlInsert, map);

			return (recordsAdded != 0);
		}

		public List<ProjectCompoundLibraryToLibraryItemMap> Get(IDbConnection connection, long projectCompoundLibraryId)
		{
			return connection.Query<ProjectCompoundLibraryToLibraryItemMap>(
				_sqlSelect + $"WHERE {ProjectCompoundLibraryIdColumn} = @ProjectCompoundLibraryId",
				new {ProjectCompoundLibraryId = projectCompoundLibraryId}).ToList();
		}

		public void Delete(IDbConnection connection, long projectCompoundLibraryId, long compoundLibraryItemId)
		{
			connection.Execute(
				$"DELETE FROM {TableName} " +
				$"WHERE {ProjectCompoundLibraryIdColumn} = @ProjectCompoundLibraryId AND {CompoundLibraryItemIdColumn} = @CompoundLibraryItemId",
				new { ProjectCompoundLibraryId = projectCompoundLibraryId, CompoundLibraryItemId = compoundLibraryItemId});
		}
		public bool HasCompoundLibraryItemId(IDbConnection connection, long compoundLibraryItemId)
		{
			var map = connection.QueryFirstOrDefault<ProjectCompoundLibraryToLibraryItemMap>(
				$"{_sqlSelect} " +
				$"WHERE {CompoundLibraryItemIdColumn} = @Id",
				new { Id = compoundLibraryItemId });

			return map != null;
		}
	}
}
