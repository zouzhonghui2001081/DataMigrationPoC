using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class SnapshotCompoundLibraryToLibraryItemMapDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "SnapshotCompoundLibraryToLibraryItemMap";
		internal static string IdColumn { get; } = "Id";
		internal static string SnapshotCompoundLibraryIdColumn { get; } = "SnapshotCompoundLibraryId";
		internal static string CompoundLibraryItemIdColumn { get; } = "CompoundLibraryItemId";

		private readonly string _sqlInsert =
			$"INSERT INTO {TableName} " +
			$"({SnapshotCompoundLibraryIdColumn}," +
			$"{CompoundLibraryItemIdColumn})" +
			"VALUES " +
			$"(@{SnapshotCompoundLibraryIdColumn}," +
			$"@{CompoundLibraryItemIdColumn}) ";

		private readonly string _sqlSelect =
			"SELECT " +
			$"{TableName}.{IdColumn}," +
			$"{TableName}.{SnapshotCompoundLibraryIdColumn}," +
			$"{TableName}.{CompoundLibraryItemIdColumn} " +
			$"FROM {TableName} ";

		public bool Create(IDbConnection connection, SnapshotCompoundLibraryToLibraryItemMap map)
		{
			var recordsAdded = connection.Execute(_sqlInsert, map);

			return (recordsAdded != 0);
		}

		public bool HasCompoundLibraryItemId(IDbConnection connection, long compoundLibraryItemId)
		{
			var map =connection.QueryFirstOrDefault<SnapshotCompoundLibraryToLibraryItemMap>(
				$"{_sqlSelect} " +
				$"WHERE {CompoundLibraryItemIdColumn} = @Id",
				new {Id = compoundLibraryItemId});

			return map != null;
		}
		public List<SnapshotCompoundLibraryToLibraryItemMap> Get(IDbConnection connection, long snapshotCompoundLibraryId)
		{
			return connection.Query<SnapshotCompoundLibraryToLibraryItemMap>(
				_sqlSelect + $"WHERE {SnapshotCompoundLibraryIdColumn} = @SnapshotCompoundLibraryId",
				new { SnapshotCompoundLibraryId = snapshotCompoundLibraryId}).ToList();
		}

		public void Delete(IDbConnection connection, long snapshotCompoundLibraryId, long compoundLibraryItemId)
		{
			connection.Execute(
				$"DELETE FROM {TableName} " +
				$"WHERE {SnapshotCompoundLibraryIdColumn} = @SnapshotCompoundLibraryId AND {CompoundLibraryItemIdColumn} = @CompoundLibraryItemId",
				new { SnapshotCompoundLibraryId = snapshotCompoundLibraryId, CompoundLibraryItemId = compoundLibraryItemId });
		}
	}
}
