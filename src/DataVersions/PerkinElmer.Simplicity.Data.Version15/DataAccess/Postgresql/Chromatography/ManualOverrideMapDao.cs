using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class ManualOverrideMapDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "ManualOverrideMap";
		internal static string IdColumn { get; } = "Id";
		internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
		internal static string BatchRunChannelGuidColumn { get; } = "BatchRunChannelGuid";
		internal static string BatchRunGuidColumn { get; } = "BatchRunGuid";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({AnalysisResultSetIdColumn}," +
		                                      $"{BatchRunChannelGuidColumn}," +
		                                      $"{BatchRunGuidColumn})" +
		                                      "VALUES " +
		                                      $"(@{AnalysisResultSetIdColumn}," +
		                                      $"@{BatchRunChannelGuidColumn}," +
		                                      $"@{BatchRunGuidColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{AnalysisResultSetIdColumn}," +
		                                      $"{BatchRunChannelGuidColumn}," +
		                                      $"{BatchRunGuidColumn} " +
		                                      $"FROM {TableName} ";

		public virtual void Create(IDbConnection connection, ManualOverrideMap manualOverrideMap)
		{
			try
			{
				manualOverrideMap.Id = connection.ExecuteScalar<long>(
					InsertSql + "RETURNING Id", manualOverrideMap);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public List<ManualOverrideMap> GetManualOverrideMapByAnalysisResultSetId(IDbConnection connection, long analysisResultId)
		{
			try
			{
				return connection.Query<ManualOverrideMap>(SelectSql +
				                                           $"WHERE {AnalysisResultSetIdColumn} = {analysisResultId}").ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetManualOverrideMapByAnalysisResultSetId method", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, long analysisResultId)
		{
			connection.Execute($"DELETE FROM {TableName} " +
			                   $"WHERE {AnalysisResultSetIdColumn} = {analysisResultId}");
		}
	}
}
