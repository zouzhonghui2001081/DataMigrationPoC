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
	internal class BatchResultSetToProcessingMethodMapDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "BatchResultSetToProcessingMethodMap";
		internal static string IdColumn { get; } = "Id";
		internal static string BatchResultIdSetColumn { get; } = "BatchResultSetId";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";

		public void Create(IDbConnection connection, BatchResultSetToProcessingMethodMap map)
		{
			try
			{
				connection.Execute(
					$"INSERT INTO {TableName} ({BatchResultIdSetColumn}," +
					$"{ProcessingMethodIdColumn}) " +
					$"VALUES (@{BatchResultIdSetColumn}," +
					$"@{ProcessingMethodIdColumn})", map);
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateChildren method", ex);
				throw;
			}
		}
		public List<BatchResultSetToProcessingMethodMap> GetByBatchResultSetId(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				return connection.Query<BatchResultSetToProcessingMethodMap>($"SELECT {IdColumn}," +
				                                                              $"{BatchResultIdSetColumn}," +
				                                                              $"{ProcessingMethodIdColumn} " +
				                                                              $"FROM {TableName} " +
				                                                              $"WHERE {BatchResultIdSetColumn} = {batchResultSetId}").ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByBatchResultSetId method", ex);
				throw;
			}
		}

		public BatchResultSetToProcessingMethodMap[] GetProcessingMethodByBatchResultSetId(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				return connection.Query<BatchResultSetToProcessingMethodMap>($"SELECT {IdColumn}," +
				                                                              $"{BatchResultIdSetColumn}," +
				                                                              $"{ProcessingMethodIdColumn} " +
				                                                              $"FROM {TableName} " +
				                                                              $"WHERE {BatchResultIdSetColumn} = {batchResultSetId}").ToArray();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByBatchResultSetId method", ex);
				throw;
			}
		}
		public List<long> GetProcessingMethodIds(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				var processingMethodIds = connection.Query<long>($"SELECT {ProcessingMethodIdColumn} " +
				                                                 $"FROM {TableName} " +
				                                                 $"WHERE {BatchResultIdSetColumn}={batchResultSetId}").ToList();

				return processingMethodIds;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodIds method", ex);
				throw;
			}
		}
		public void DeleteByBatchResultSetId(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} " +
				                   $"WHERE {BatchResultIdSetColumn}={batchResultSetId}");

			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteByBatchResultSetId method", ex);
				throw;
			}
		}
	}
}
