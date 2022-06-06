using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    
	internal class CalculatedChannelDataDao : ChannelDataDaoBase
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "CalculatedChannelData";

		public static string BatchRunAnalysisResultIdColumn { get; } = "BatchRunAnalysisResultId";
		public static string BatchRunChannelGuidColumn { get; } = "BatchRunChannelGuid";

		protected readonly string InsertSql = $"INSERT INTO {TableName} ({BatchRunAnalysisResultIdColumn}," +
		                                      $"{BatchRunChannelGuidColumn}," +
		                                      $"{BlankSubtractionAppliedColumn}," +
		                                      $"{SmoothAppliedColumn}) " +
		                                      $"VALUES (@{BatchRunAnalysisResultIdColumn}," +
		                                      $"@{BatchRunChannelGuidColumn}," +
		                                      $"@{BlankSubtractionAppliedColumn}," +
		                                      $"@{SmoothAppliedColumn}) ";
		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{BatchRunAnalysisResultIdColumn}," +
		                                      $"{BatchRunChannelGuidColumn}," +
		                                      $"{BlankSubtractionAppliedColumn}," +
		                                      $"{SmoothAppliedColumn} " +
		                                      $"FROM {TableName} ";
		public IList<CalculatedChannelData> GetChannelDataByBatchRunAnalysisResultId(IDbConnection connection, long batchRunAnalysisResultId)
		{
			try
			{

				var calculatedChannelData = connection.Query<CalculatedChannelData>(
					SelectSql + $"WHERE { BatchRunAnalysisResultIdColumn} = { batchRunAnalysisResultId}").ToList();

                return calculatedChannelData;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetChannelDataByBatchRunAnalysisResultId method", ex);
				throw;
			}
		}

		public long SaveCalculatedChannelData(IDbConnection connection, CalculatedChannelData calculatedChannelData)
		{
			long calculatedChannelDataId = 0;
			try
			{
				long existingCalculatedChannelDataId =
					connection.QueryFirstOrDefault<long>(
						$"SELECT {IdColumn} " +
						$"FROM {TableName} " +
                        $"WHERE {BatchRunAnalysisResultIdColumn} = {calculatedChannelData.BatchRunAnalysisResultId} and {BatchRunChannelGuidColumn} = '{calculatedChannelData.BatchRunChannelGuid}'"
                    );

				if (existingCalculatedChannelDataId <= 0)
				{
					calculatedChannelDataId =
						connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", calculatedChannelData);
				}
				else
				{
					calculatedChannelDataId = existingCalculatedChannelDataId;
				}

			}
			catch (Exception ex)
			{
				Log.Error("Error in SaveCalculatedChannelData method", ex);
				throw;
			}

			return calculatedChannelDataId;

		}

		public CalculatedChannelData GetChannelDataModifiableIdsByBatchRunAnalysisResultId(IDbConnection connection, long channelDataModifiableId)
		{
			try
			{
				var channelDataModifiables =
					connection.Query<CalculatedChannelData>(
						SelectSql + $" WHERE {IdColumn} = {channelDataModifiableId}").ToList();
				return channelDataModifiables.FirstOrDefault();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetChannelDataModifiableIdsByBatchRunAnalysisResultId method", ex);
				throw;
			}
		}

		public List<long> GetChannelModifiableIdsByBatchRunAnalysisResultId(IDbConnection connection,
			long bacthRunAnalysisResultId)
		{
			try
			{
				var channelMethodModifiableIds = connection
					.Query<long>(
						$"Select {IdColumn} from {TableName} where {BatchRunAnalysisResultIdColumn} ={bacthRunAnalysisResultId}")
					.ToList();

				return channelMethodModifiableIds;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetChannelModifiableIdsByBatchRunAnalysisResultId method", ex);
				throw;
			}

		}

		public bool RemoveChannelDataModifiable(IDbConnection connection, long channelDataModifiableId)
		{
			try
			{
				connection.Execute($"Delete from {TableName} where {IdColumn} = {channelDataModifiableId}");
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("Error in RemoveChannelDataModifiable method", ex);
				throw;
			}
		}
	}
}
