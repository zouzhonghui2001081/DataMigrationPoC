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
    internal class ChannelDataDao : ChannelDataDaoBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string TableName { get; } = "ChannelData";

        public static string BatchRunIdColumn { get; } = "BatchRunId";

        public ChannelData GetChannelDataByBatchRunIds(IDbConnection connection, long batchRunId)
        {
            try
            {
				return connection.Query<ChannelData>($"SELECT " +
				                                     $"{IdColumn}," +
				                                     $"{BatchRunIdColumn}," +
				                                     $"{ChannelTypeColumn}," +
				                                     $"{ChannelDataTypeColumn}," +
				                                     $"{ChannelIndexColumn}," +
				                                     $"{ChannelMetaDataColumn}," +
				                                     $"{RawChannelTypeColumn}," +
				                                     $"{BlankSubtractionAppliedColumn}," +
				                                     $"{SmoothAppliedColumn} " +
				                                     $"FROM {TableName} WHERE {BatchRunIdColumn} = {batchRunId}").FirstOrDefault();

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetChannelDataByBatchRunId method", ex);
                throw;
            }
        }

	    public void InsertChannelData(IDbConnection connection, ChannelData channelData)
	    {
		    try
		    {
			    channelData.Id = connection.ExecuteScalar<long>(
				    $"INSERT INTO {TableName} (" +
				    $"{BatchRunIdColumn}," +
				    $"{ChannelDataTypeColumn}," +
				    $"{ChannelTypeColumn}," +
				    $"{ChannelIndexColumn}," +
				    $"{ChannelMetaDataColumn}," +
				    $"{RawChannelTypeColumn}," +
				    $"{BlankSubtractionAppliedColumn}," +
				    $"{SmoothAppliedColumn}" +
				    ") " +
				    "VALUES (" +
				    $"@{BatchRunIdColumn}," +
				    $"@{ChannelDataTypeColumn}," +
				    $"@{ChannelTypeColumn}," +
				    $"@{ChannelIndexColumn}," +
				    $"@{ChannelMetaDataColumn}," +
				    $"@{RawChannelTypeColumn}," +
				    $"@{BlankSubtractionAppliedColumn}," +
				    $"@{SmoothAppliedColumn}" +
				    ") RETURNING Id", channelData);
		    }
		    catch (Exception ex)
		    {
				Log.Error("Error in InsertBatchChannel method", ex);
			    throw;
			}
	    }
	    public void InsertChannelData(IDbConnection connection, ChannelData[] channelData)
	    {
		    try
		    {
			    connection.Execute(
				    $"INSERT INTO {TableName} (" +
				    $"{BatchRunIdColumn}," +
				    $"{ChannelDataTypeColumn}," +
				    $"{ChannelTypeColumn}," +
				    $"{ChannelIndexColumn}," +
				    $"{ChannelMetaDataColumn}," +
				    $"{RawChannelTypeColumn}," +
				    $"{BlankSubtractionAppliedColumn}," +
				    $"{SmoothAppliedColumn}) " +
				    "VALUES (" +
				    $"@{BatchRunIdColumn}," +
				    $"@{ChannelDataTypeColumn}," +
				    $"@{ChannelTypeColumn}," +
				    $"@{ChannelIndexColumn}," +
				    $"@{ChannelMetaDataColumn}," +
				    $"@{RawChannelTypeColumn}," +
				    $"@{BlankSubtractionAppliedColumn}," +
				    $"@{SmoothAppliedColumn}" +
				    ")", channelData);
		    }
		    catch (Exception ex)
		    {
			    Log.Error("Error in InsertBatchChannel method", ex);
			    throw;
		    }
	    }

        public List<ChannelData> GetChannelDataByBatchRunId(IDbConnection connection, long batchRunId)
        {
            try
            {
                List<ChannelData> channelData =
                    connection.Query<ChannelData>($"SELECT " +
                                                  $"{IdColumn}," +
                                                  $"{BatchRunIdColumn}," +
                                                  $"{ChannelTypeColumn}," +
                                                  $"{ChannelDataTypeColumn}," +
                                                  $"{ChannelIndexColumn}," +
                                                  $"{ChannelMetaDataColumn}," +
                                                  $"{RawChannelTypeColumn}," +
                                                  $"{BlankSubtractionAppliedColumn}," +
                                                  $"{SmoothAppliedColumn} " +
                                                  $"FROM {TableName} " +
                                                  $"WHERE {BatchRunIdColumn} = {batchRunId} ").ToList();
                return channelData;

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetChannelDataByBatchRunId method", ex);
                throw;
            }
        }

        public void SaveChannelData(IDbConnection connection, ChannelData channelData)
        {
            try
            {
                ChannelData chData = GetChannelData(connection, channelData);
                if (chData == null)
                {
                    InsertChannelData(connection, channelData);
                }
                else
                {
                    connection.Execute(
                        $"DELETE FROM {TableName} " +
                        $"WHERE {IdColumn} = {chData.Id}"
                    );
                    InsertChannelData(connection, channelData);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in SaveChannelData method", ex);
                throw;
            }
        }

        private ChannelData GetChannelData(IDbConnection connection, ChannelData channelData)
        {
            try
            {
                ChannelData chData = connection.Query<ChannelData>($"SELECT * FROM {TableName} " +
                                                           $"WHERE {BatchRunIdColumn} = {channelData.BatchRunId}  " +
                                                           $"AND {ChannelIndexColumn} = {channelData.ChannelIndex}").FirstOrDefault();
                return chData;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetChannelData method", ex);
                throw;
            }
        }
    }
}
