using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class StreamDataBatchResultDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "StreamDataBatchResult";
		public static string IdColumn { get; } = "Id";
		public static string BatchRunIdColumn { get; } = "BatchRunId";
		public static string StreamIndexColumn { get; } = "StreamIndex";
		public static string MetaDataColumn { get; } = "MetaData";
		public static string MetaDataTypeColumn { get; } = "MetaDataType";
		public static string YDataColumn { get; } = "YData";
		public static string DeviceIdColumn { get; } = "DeviceId";
		public static string LargeObjectOidColumn { get; } = "LargeObjectOid";
		public static string UseLargeObjectStreamColumn { get; } = "UseLargeObjectStream";
		public static string FirmwareVersionColumn { get; } = "FirmwareVersion";
		public static string SerialNumberColumn { get; } = "SerialNumber";
		public static string ModelNameColumn { get; } = "ModelName";
		public static string UniqueIdentifierColumn { get; } = "UniqueIdentifier";
		public static string InterfaceAddressColumn { get; } = "InterfaceAddress";
		protected readonly string InsertSql = $"INSERT INTO {TableName} (" +
		                                      $"{BatchRunIdColumn}," +
		                                      $"{StreamIndexColumn}," +
		                                      $"{MetaDataColumn}," +
		                                      $"{MetaDataTypeColumn}," +
		                                      $"{YDataColumn}," +
		                                      $"{DeviceIdColumn}," +
		                                      $"{LargeObjectOidColumn}," +
		                                      $"{UseLargeObjectStreamColumn}," +
		                                      $"{FirmwareVersionColumn}," +
		                                      $"{SerialNumberColumn}," +
		                                      $"{ModelNameColumn}," +
		                                      $"{UniqueIdentifierColumn}," +
		                                      $"{InterfaceAddressColumn}) " +
		                                      $"VALUES (@{BatchRunIdColumn}," +
		                                      $"@{StreamIndexColumn}," +
		                                      $"@{MetaDataColumn}," +
		                                      $"@{MetaDataTypeColumn}," +
		                                      $"@{YDataColumn}," +
		                                      $"@{DeviceIdColumn}," +
		                                      $"@{LargeObjectOidColumn}," +
		                                      $"@{UseLargeObjectStreamColumn}," +
		                                      $"@{FirmwareVersionColumn}," +
		                                      $"@{SerialNumberColumn}," +
		                                      $"@{ModelNameColumn}," +
		                                      $"@{UniqueIdentifierColumn}," +
		                                      $"@{InterfaceAddressColumn}) ";
		public void InsertStreamDatas(IDbConnection connection, StreamDataBatchResult[] streamData)
		{
			try
			{
				connection.Execute(InsertSql, streamData);
			}
			catch (Exception ex)
			{
				Log.Error("Error in InsertStreamDatas method", ex);
				throw;
			}
		}
		public void InsertStreamData(IDbConnection connection, StreamDataBatchResult streamData)
		{
			try
			{
				connection.Execute(InsertSql, streamData);
			}
			catch (Exception ex)
			{
				Log.Error("Error in InsertStreamData method", ex);
				throw;
			}
		}
		public bool AddStreamData(IDbConnection connection, Guid batchRunGuid, string deviceID, int streamIndex, byte[] dataChunk)
		{
			try
			{
				BatchRunDao batchRunDao = new BatchRunDao();
				long? batchRunId = batchRunDao.GetBatchRunIdByGuid(connection, batchRunGuid);
				int rowsAffected = 0;

				if (batchRunId.HasValue)
				{
					rowsAffected = connection.Execute(
						$"UPDATE {TableName} " +
						$"SET {YDataColumn} = {YDataColumn} || @{YDataColumn} " +
						$"WHERE {BatchRunIdColumn} = {batchRunId.Value} AND {DeviceIdColumn} = @DeviceID AND {StreamIndexColumn} = {streamIndex}",
						new { YData = dataChunk , DeviceID  = deviceID });
				}

				return (rowsAffected > 0);
			}
			catch (Exception ex)
			{
				Log.Error("Error in AddStreamData method", ex);
				throw;
			}
		}
		public (bool isSucceeded, string errorMessage, byte[] data) GetStreamData(IDbConnection connection,
            Guid batchRunGuid,
            string deviceId, int streamIndex)
		{
			try
			{
				byte[] data = null;
				var item = connection.QueryFirstOrDefault(
                    $"SELECT {TableName}.{UseLargeObjectStreamColumn}," +
                    $"{TableName}.{LargeObjectOidColumn} " +
                    $"FROM {TableName} " +
                    $"INNER JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.IdColumn} = {TableName}.{BatchRunIdColumn} " +
                    $"WHERE {BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = @BatchRunGuid AND {TableName}.{StreamIndexColumn} = {streamIndex} AND {TableName}.{DeviceIdColumn} = @DeviceID"
					,new { DeviceID = deviceId, BatchRunGuid = batchRunGuid });

				if (item != null && item.uselargeobjectstream)
				{
					var manager = new NpgsqlLargeObjectManager((NpgsqlConnection) connection);

					using (var stream = manager.OpenRead((uint) item.largeobjectoid))
					{
						int size = (int) stream.Length;
						data = new byte[size];
						stream.Read(data, 0, size);
						stream.Close();
					}
				}
				else
                {
                    var sql = $"SELECT {TableName}.{YDataColumn} " +
                              $"FROM {TableName} " +
                              $"INNER JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.IdColumn} = {TableName}.{BatchRunIdColumn} " +
                              $"WHERE {BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = @BatchRunGuid AND {TableName}.{DeviceIdColumn} = @DeviceID  AND {TableName}.{StreamIndexColumn} = {streamIndex}";
                    data = connection.QueryFirstOrDefault<byte[]>(sql, new { DeviceID = deviceId, BatchRunGuid = batchRunGuid }).ToArray();
                }

				return (true, string.Empty, data);
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetStreamData method", ex);
				return (false, ex.Message, null);
			}
		}

		public IList<StreamDataBatchResult> GetStreamInfo(IDbConnection connection, Guid batchRunGuid)
        {
            return connection.Query<StreamDataBatchResult>(
				$"SELECT {TableName}.{StreamIndexColumn}," +
				$"{TableName}.{MetaDataColumn}," +
				$"{TableName}.{MetaDataTypeColumn}," +
				$"{TableName}.{DeviceIdColumn}," +
				$"{TableName}.{UseLargeObjectStreamColumn}," +
				$"{TableName}.{FirmwareVersionColumn}," +
				$"{TableName}.{SerialNumberColumn}," +
				$"{TableName}.{ModelNameColumn}," +
				$"{TableName}.{UniqueIdentifierColumn}," +
				$"{TableName}.{InterfaceAddressColumn} " +
                $"FROM {TableName} " +
                $"INNER JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.IdColumn} = {TableName}.{BatchRunIdColumn} " +
                $"WHERE {BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = '{batchRunGuid}'").ToList();
        }

        public IList<StreamDataBatchResult> GetStreamDataWithoutYData(IDbConnection connection, long batchRunId)
        {
            Log.Info($"Invoked GetStreamData(): batchRunId={batchRunId}");
            try
            {
                var result = connection.Query<StreamDataBatchResult>(
                    $"SELECT " +
                    $"{IdColumn}," +
                    $"{StreamIndexColumn}," +
                    $"{MetaDataColumn}," +
                    $"{MetaDataTypeColumn}," +
                    $"{BatchRunIdColumn}," +
                    $"{DeviceIdColumn}," +
                    $"{UseLargeObjectStreamColumn}," +
                    $"{FirmwareVersionColumn}," +
                    $"{SerialNumberColumn}," +
                    $"{ModelNameColumn}," +
                    $"{UniqueIdentifierColumn}," +
                    $"{InterfaceAddressColumn} " +
                    $"FROM {TableName} " +
                    $"WHERE {BatchRunIdColumn} = '{batchRunId}'").ToList();
                Log.Debug($"{result.Count} records found");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error occurred in GetStreamData()", ex);
                throw;
            }
        }

        public void DeleteLargeObjects(IDbConnection connection, long batchResultId)
        {
	        try
	        {
				var largeObjects = connection.Query<StreamDataBatchResult>(
					"SELECT " +
					$"{TableName}.{IdColumn}," +
					$"{TableName}.{StreamIndexColumn}," +
					$"{TableName}.{MetaDataColumn}," +
					$"{TableName}.{MetaDataTypeColumn}," +
					$"{TableName}.{BatchRunIdColumn}," +
					$"{TableName}.{DeviceIdColumn}," +
					$"{TableName}.{LargeObjectOidColumn}," +
					$"{TableName}.{UseLargeObjectStreamColumn} " +
					$"FROM {BatchRunDao.TableName} " +
					$"INNER JOIN {BatchResultSetDao.TableName} ON {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} = {BatchRunDao.TableName}.{BatchRunDao.BatchResultSetIdColumn} " +
					$"INNER JOIN {TableName} ON {TableName}.{BatchRunIdColumn} = {BatchRunDao.TableName}.{BatchRunDao.IdColumn} " +
					$"WHERE {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} = @BatchResultId AND " +
					$"{TableName}.{UseLargeObjectStreamColumn} = @{UseLargeObjectStreamColumn}",
					new { BatchResultId = batchResultId, UseLargeObjectStream = true }).ToList();

				var manager = new NpgsqlLargeObjectManager((NpgsqlConnection)connection);

				foreach (var largeObject in largeObjects)
				{
					if (largeObject.LargeObjectOid.HasValue)
						manager.Unlink((uint)largeObject.LargeObjectOid.Value);
				}
	        }
	        catch (Exception ex)
	        {
		        Log.Error($"Error occurred in DeleteLargeObjects()", ex);
		        throw;
	        }
		}

        public bool UpdateStreamDataBatchResult(IDbConnection connection,
	        Guid projectGuid,
	        Guid batchResultSetGuid,
	        Guid batchRunGuid,
	        string streamDataDeviceId,
	        int streamId,
	        StreamDataBatchResult streamDataBatchResult)
        {
	        try
	        {
		        var sql =
			        $"SELECT {TableName}.{IdColumn} " +
			        $"FROM {ProjectDaoBase.TableName} " +
			        $"INNER JOIN {BatchResultSetDao.TableName} ON {BatchResultSetDao.TableName}.{BatchResultSetDao.ProjectIdColumn} = {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} " +
			        $"INNER JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.BatchResultSetIdColumn} = {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} " +
			        $"INNER JOIN {TableName} ON {TableName}.{BatchRunIdColumn} = {BatchRunDao.TableName}.{BatchRunDao.IdColumn} " +
			        $"WHERE {ProjectDaoBase.TableName}.{ProjectDaoBase.GuidColumn} = @ProjectGuid AND " +
			        $"{BatchResultSetDao.TableName}.{BatchResultSetDao.GuidColumn} = @BatchResultGuid AND " +
			        $"{BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = @BatchRunGuid AND " +
			        $"{TableName}.{StreamIndexColumn} = @StreamIndex AND " +
			        $"{TableName}.{DeviceIdColumn} = @DeviceId";

		        streamDataBatchResult.Id = connection.ExecuteScalar<long>(
			        sql,
			        new
			        {
				        ProjectGuid = projectGuid,
				        BatchResultGuid = batchResultSetGuid,
				        BatchRunGuid = batchRunGuid,
				        StreamIndex = streamId,
				        DeviceId = streamDataDeviceId,
			        });

		        if (streamDataBatchResult.Id == 0) return false;

		        var rowsUpdated = connection.Execute(
			        $"UPDATE {TableName} " +
			        $"SET {MetaDataColumn} = @MetaData," +
			        $"{MetaDataTypeColumn} = @MetaDataType," +
			        $"{FirmwareVersionColumn} = @FirmwareVersion," +
			        $"{SerialNumberColumn} = @SerialNumber," +
			        $"{ModelNameColumn} = @ModelName," +
			        $"{UniqueIdentifierColumn} = @UniqueIdentifier," +
			        $"{InterfaceAddressColumn} = @InterfaceAddress " +
			        $"WHERE {IdColumn} = @Id",
			        streamDataBatchResult);

		        return rowsUpdated != 0;
	        }
	        catch (Exception ex)
	        {
		        Log.Error($"Error occurred in UpdateStreamDataBatchResult()", ex);
		        throw;
	        }
		}
	}
}
