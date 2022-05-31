using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class SchemaVersionDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static string TableName { get; } = "SchemaVersion";
		internal static string MajorVersionColumn { get; } = "MajorVersion";
		internal static string MinorVersionColumn { get; } = "MinorVersion";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			$"({MajorVersionColumn}," +
			$"{MinorVersionColumn}) " +
			"VALUES" +
			$"(@{MajorVersionColumn}," +
			$"@{MinorVersionColumn}) ";

		protected readonly string SelectSql =
			"SELECT " +
			$"{MajorVersionColumn},{MinorVersionColumn} " +
			$"FROM {TableName} ";

		public SchemaVersion GetSchemaVersion(IDbConnection connection)
		{
			try
			{
				return connection.QueryFirstOrDefault<SchemaVersion>(
					SelectSql + 
					$"WHERE {MajorVersionColumn} != @DataMajorVersion",
					new { DataMajorVersion = -1});
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetSchemaVersion method", ex);
				throw;
			}

		}

		public SchemaVersion GetDataVersion(IDbConnection connection)
		{
			try
			{
				return connection.QueryFirstOrDefault<SchemaVersion>(
					SelectSql +
					$"WHERE {MajorVersionColumn} = @MajorVersion",
					new {MajorVersion = -1});
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetDataVersion method", ex);
				throw;
			}

		}
		public bool CreateDataVersion(IDbConnection connection, int majorVersion, int minorVersion)
		{
			var rowsAdded = connection.Execute(
				InsertSql, new { MajorVersion = majorVersion, MinorVersion = minorVersion });

			return rowsAdded != 0;
		}
		public bool UpdateDataVersion(IDbConnection connection, int majorVersion, int minorVersion)
		{
			try
			{
				var rowsUpdated = connection.Execute(
					$"UPDATE {TableName} " +
					$"SET {MajorVersionColumn} = @MajorVersion, {MinorVersionColumn} = @MinorVersion " +
					$"WHERE {MajorVersionColumn} = @MajorVersion",
					new { MajorVersion = majorVersion, MinorVersion = minorVersion});

				return rowsUpdated != 0;
			}
			catch (Exception ex)
			{
				Log.Error("Error in UpdateDataVersion method", ex);
				throw;
			}
		}
	}
}
