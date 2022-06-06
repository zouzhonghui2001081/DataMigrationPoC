using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class CalibrationBatchRunInfoDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "CalibrationBatchRunInfo";
		internal static string IdColumn { get; } = "Id";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		internal static string KeyColumn { get; } = "Key";
		internal static string BatchRunGuidColumn { get; } = "BatchRunGuid";
		internal static string BatchResultSetGuidColumn { get; } = "BatchResultSetGuid";
		internal static string BatchRunNameColumn { get; } = "BatchRunName";
		internal static string ResultSetNameColumn { get; } = "ResultSetName";
		internal static string BatchRunAcquisitionTimeColumn { get; } = "BatchRunAcquisitionTime";

		protected readonly string SelectSql = "SELECT " +
		                                  $"{IdColumn}," +
		                                  $"{ProcessingMethodIdColumn}," +
		                                  $"{KeyColumn}," +
		                                  $"{BatchRunGuidColumn}," +
		                                  $"{BatchResultSetGuidColumn}," +
		                                  $"{BatchRunNameColumn}," +
		                                  $"{ResultSetNameColumn}," +
		                                  $"{BatchRunAcquisitionTimeColumn} " +
		                                  $"FROM {TableName} ";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			$"({ProcessingMethodIdColumn}," +
			$"{KeyColumn}," +
			$"{BatchRunGuidColumn}," +
			$"{BatchResultSetGuidColumn}," +
			$"{BatchRunNameColumn}," +
			$"{ResultSetNameColumn}," +
			$"{BatchRunAcquisitionTimeColumn}) " +
			"VALUES " +
			$"(@{ProcessingMethodIdColumn}," +
			$"@{KeyColumn}," +
			$"@{BatchRunGuidColumn}," +
			$"@{BatchResultSetGuidColumn}," +
			$"@{BatchRunNameColumn}," +
			$"@{ResultSetNameColumn}," +
			$"@{BatchRunAcquisitionTimeColumn}) ";

		public virtual void Create(IDbConnection connection, List<CalibrationBatchRunInfo> calibrationBatchRunInfos)
        {
            try
            {
	            foreach (var calibrationBatchRunInfo in calibrationBatchRunInfos)
	            {
				    calibrationBatchRunInfo.Id = connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", calibrationBatchRunInfo);
	            }
            }
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}
		public List<CalibrationBatchRunInfo> Get(IDbConnection connection,
		    long processingMethodId)
		{
			try
			{
				return connection.Query<CalibrationBatchRunInfo>(
					SelectSql +
					$"WHERE {TableName}.{ProcessingMethodIdColumn} = @ProcessingMethodId",
					new { ProcessingMethodId = processingMethodId}).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Get method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection,
			long processingMethodId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {ProcessingMethodIdColumn} = @ProcessingMethodId",
					new { ProcessingMethodId = processingMethodId });
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
	}
}
