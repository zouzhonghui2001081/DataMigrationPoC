using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class BatchResultSetToAcquisitionMethodMapDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "BatchResultSetToAcquisitionMethodMap";
		internal static string IdColumn { get; } = "Id";
		internal static string BatchResultIdSetColumn { get; } = "BatchResultSetId";
		internal static string AcquisitionMethodIdColumn { get; } = "AcquisitionMethodId";

		public void Create(IDbConnection connection, BatchResultSetToAcquisitionMethodMap batchResultSetToAcquisitionMethodMap)
		{
			try
			{
				connection.Execute(
					$"INSERT INTO {TableName} ({BatchResultIdSetColumn}," +
					$"{AcquisitionMethodIdColumn}) " +
					$"VALUES (@{BatchResultIdSetColumn}," +
					$"@{AcquisitionMethodIdColumn})", batchResultSetToAcquisitionMethodMap);
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateChildren method", ex);
				throw;
			}
		}

		public BatchResultSetToAcquisitionMethodMap[] GetByBatchResultSetId(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				return connection.Query<BatchResultSetToAcquisitionMethodMap>($"SELECT {IdColumn}," +
				                                                       $"{BatchResultIdSetColumn}," +
				                                                       $"{AcquisitionMethodIdColumn} " +
				                                                       $"FROM {TableName} " +
				                                                       $"WHERE {BatchResultIdSetColumn} = {batchResultSetId}").ToArray();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByBatchResultSetId method", ex);
				throw;
			}
		}
		public List<long> GetAcquisitionMethodIds(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				var acquisitionMethodIds = 
					connection.Query<long>($"SELECT {AcquisitionMethodIdColumn} " +
					                       $"FROM {TableName} " +
					                       $"WHERE {BatchResultIdSetColumn} = @{BatchResultIdSetColumn}",
						new { BatchResultSetId = batchResultSetId}).ToList();

				return acquisitionMethodIds;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethodIds method", ex);
				throw;
			}
		}

		public void DeleteByBatchResultSetId(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} " +
				                   $"WHERE {BatchResultIdSetColumn} = @{BatchResultIdSetColumn}", 
					new { BatchResultSetId = batchResultSetId});
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteByAnalysisResultSetId method", ex);
				throw;
			}
		}
	}
}
