using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class BatchRunChannelMapDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "BatchRunChannelMap";
		internal static string IdColumn { get; } = "Id";
		internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
		internal static string BatchRunChannelGuidColumn { get; } = "BatchRunChannelGuid";
		internal static string BatchRunGuidColumn { get; } = "BatchRunGuid";
		internal static string OriginalBatchRunGuidColumn { get; } = "OriginalBatchRunGuid";
		internal static string BatchRunChannelDescriptorTypeColumn { get; } = "BatchRunChannelDescriptorType";
		internal static string BatchRunChannelDescriptorColumn { get; } = "BatchRunChannelDescriptor";
		internal static string ProcessingMethodGuidColumn { get; } = "ProcessingMethodGuid";
		internal static string ProcessingMethodChannelGuidColumn { get; } = "ProcessingMethodChannelGuid";
		internal static string XDataColumn { get; } = "XData";
		internal static string YDataColumn { get; } = "YData";

		protected readonly string InsertSql = $"INSERT INTO {TableName} ({AnalysisResultSetIdColumn}," +
					$"{BatchRunChannelGuidColumn}," +
					$"{BatchRunGuidColumn}," +
					$"{OriginalBatchRunGuidColumn}," +
					$"{BatchRunChannelDescriptorColumn}," +
					$"{BatchRunChannelDescriptorTypeColumn}," +
					$"{ProcessingMethodGuidColumn}," +
					$"{ProcessingMethodChannelGuidColumn}," +
					$"{XDataColumn}," +
					$"{YDataColumn}) " +
					$"VALUES (@{AnalysisResultSetIdColumn}," +
					$"@{BatchRunChannelGuidColumn}," +
					$"@{BatchRunGuidColumn}," +
					$"@{OriginalBatchRunGuidColumn}," +
					$"@{BatchRunChannelDescriptorColumn}," +
					$"@{BatchRunChannelDescriptorTypeColumn}," +
					$"@{ProcessingMethodGuidColumn}," +
					$"@{ProcessingMethodChannelGuidColumn}," +
					$"@{XDataColumn}," +
		                                      $"@{YDataColumn}) ";
		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{AnalysisResultSetIdColumn}," +
		                                      $"{BatchRunChannelGuidColumn}," +
		                                      $"{BatchRunGuidColumn}," +
		                                      $"{OriginalBatchRunGuidColumn}," +
		                                      $"{BatchRunChannelDescriptorTypeColumn}," +
		                                      $"{BatchRunChannelDescriptorColumn}," +
		                                      $"{ProcessingMethodGuidColumn}," +
		                                      $"{ProcessingMethodChannelGuidColumn}," +
		                                      $"{XDataColumn}," +
		                                      $"{YDataColumn} " +
		                                      $"FROM {TableName} ";
		public virtual void Create(IDbConnection connection, BatchRunChannelMap batchRunChannelMap)
		{
			try
			{
				batchRunChannelMap.Id = connection.ExecuteScalar<long>(InsertSql + "Returning Id", batchRunChannelMap);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public List<BatchRunChannelMap> GetBatchRunChannelMapByAnalysisResultSetId(IDbConnection connection, long analysisResultId)
		{
			try
			{
				return connection.Query<BatchRunChannelMap>(SelectSql +
				                                            $"WHERE {AnalysisResultSetIdColumn} = {analysisResultId}").ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetBatchRunChannelMapByAnalysisResultSetId method", ex);
				throw;
			}
		}



		public void Delete(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {AnalysisResultSetIdColumn}={analysisResultSetId}");
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}
	}
}
