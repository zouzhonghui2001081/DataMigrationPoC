using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class LevelAmountsDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "LevelAmount";
		public static string IdColumn { get; } = "Id";
        internal static string CompoundIdColumn { get; } = "CompoundId";
		public static string LevelColumn { get; } = "Level";
		public static string AmountColumn { get; } = "Amount";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({CompoundIdColumn}," +
		                                      $"{LevelColumn}," +
		                                      $"{AmountColumn}) " +
		                                      "VALUES " +
		                                      $"(@{CompoundIdColumn}," +
		                                      $"@{LevelColumn}," +
		                                      $"@{AmountColumn}) ";

		protected readonly string SelectSql =
			"SELECT " +
			$"{IdColumn}," +
			$"{CompoundIdColumn}," +
			$"{LevelColumn}," +
			$"{AmountColumn} " +
			$"FROM {TableName} ";

		internal List<LevelAmount> GetLevelAmountsByCompoundId(IDbConnection connection, long compoundId)
		{
			Log.Debug($"Invoked GetLevelAmountsByCompoundId(): compoundId={compoundId}");
			try
			{
				var result = connection.Query<LevelAmount>(
					SelectSql +
					$"WHERE {CompoundIdColumn} = {compoundId}").ToList();

				Log.Debug($"{result.Count} records found in {TableName} table");

				return result;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in GetLevelAmountsByCompoundId()", ex);
				throw;
			}
		}

		internal virtual void Create(IDbConnection connection, IList<LevelAmount> levelAmountsBatchResultList)
		{
			Log.Debug($"Invoked Create(): levelAmountsBatchResultList.Count={levelAmountsBatchResultList.Count}");
			try
			{
				foreach (var levelAmounts in levelAmountsBatchResultList)
				{
					levelAmounts.Id = connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", levelAmounts);

				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in Create()", ex);
				throw;
			}
		}

	}
}
