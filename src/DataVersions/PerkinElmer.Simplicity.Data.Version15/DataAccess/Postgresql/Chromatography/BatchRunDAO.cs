using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class BatchRunDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "BatchRun";
		internal static string IdColumn { get; } = "Id";
		internal static string CreatedDateColumn { get; } = "CreatedDate";
		internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
		internal static string ModifiedDateColumn { get; } = "ModifiedDate";
		internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
		internal static string AcquisitionCompletionStateColumn { get; } = "AcquisitionCompletionState";
		internal static string AcquisitionTimeColumn { get; } = "AcquisitionTime";
		internal static string BatchResultSetIdColumn { get; } = "BatchResultSetId";
		internal static string NameColumn { get; } = "Name";
		internal static string GuidColumn { get; } = "Guid";
		internal static string IsBaselineRunColumn { get; } = "IsBaselineRun";
		internal static string RepeatIndexColumn { get; } = "RepeatIndex";
		internal static string SequenceSampleInfoBatchResultIdColumn { get; } = "SequenceSampleInfoBatchResultId";
		internal static string ProcessingMethodBatchResultIdColumn { get; } = "ProcessingMethodBatchResultId";
		internal static string CalibrationMethodBatchResultIdColumn { get; } = "CalibrationMethodBatchResultId";
		internal static string AcquisitionMethodIdColumn { get; } = "AcquisitionMethodBatchResultId";
		internal static string DataSourceTypeColumn { get; } = "DataSourceType";
		internal static string AcquisitionCompletionStateDetailsColumn { get; } = "AcquisitionCompletionStateDetails";
		internal static string IsModifiedAfterSubmissionColumn { get; } = "IsModifiedAfterSubmission";

		protected readonly string SelectSql = "SELECT " +
		                                  $"{IdColumn}," +
		                                  $"{CreatedDateColumn}," +
		                                  $"{CreatedUserIdColumn}," +
		                                  $"{ModifiedDateColumn}," +
		                                  $"{ModifiedUserIdColumn}," +
		                                  $"{AcquisitionCompletionStateColumn}," +
		                                  $"{AcquisitionTimeColumn}," +
		                                  $"{BatchResultSetIdColumn}," +
		                                  $"{NameColumn}," +
		                                  $"{GuidColumn}," +
		                                  $"{IsBaselineRunColumn}," +
		                                  $"{RepeatIndexColumn}," +
		                                  $"{SequenceSampleInfoBatchResultIdColumn}," +
		                                  $"{ProcessingMethodBatchResultIdColumn}," +
		                                  $"{CalibrationMethodBatchResultIdColumn}," +
		                                  $"{AcquisitionMethodIdColumn}," +
		                                  $"{DataSourceTypeColumn}," +
		                                  $"{AcquisitionCompletionStateDetailsColumn}," +
		                                  $"{IsModifiedAfterSubmissionColumn} " +
		                                  $"FROM {TableName} ";
										  
		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
                        $"({NameColumn}," +
                        $"{GuidColumn}," +
                        $"{CreatedDateColumn}," +
                        $"{CreatedUserIdColumn}," +
                        $"{ModifiedDateColumn}," +
                        $"{ModifiedUserIdColumn}," +
                        $"{IsBaselineRunColumn}," +
                        $"{RepeatIndexColumn}," +
                        $"{AcquisitionCompletionStateColumn}," +
                        $"{AcquisitionTimeColumn}," +
                        $"{SequenceSampleInfoBatchResultIdColumn}," +
                        $"{BatchResultSetIdColumn}," +
                        $"{ProcessingMethodBatchResultIdColumn}," +
                        $"{CalibrationMethodBatchResultIdColumn}," +
                        $"{AcquisitionMethodIdColumn}," +
                        $"{DataSourceTypeColumn}," +
                        $"{AcquisitionCompletionStateDetailsColumn}," +
                        $"{IsModifiedAfterSubmissionColumn}) " +
                        "VALUES " +
                        $"(@{NameColumn}," +
                        $"@{GuidColumn}," +
                        $"@{CreatedDateColumn}," +
                        $"@{CreatedUserIdColumn}," +
                        $"@{ModifiedDateColumn}," +
                        $"@{ModifiedUserIdColumn}," +
                        $"@{IsBaselineRunColumn}," +
                        $"@{RepeatIndexColumn}," +
                        $"@{AcquisitionCompletionStateColumn}," +
                        $"@{AcquisitionTimeColumn}," +
                        $"@{SequenceSampleInfoBatchResultIdColumn}," +
                        $"@{BatchResultSetIdColumn}," +
                        $"@{ProcessingMethodBatchResultIdColumn}," +
                        $"@{CalibrationMethodBatchResultIdColumn}," +
                        $"@{AcquisitionMethodIdColumn}," +
                        $"@{DataSourceTypeColumn}," +
                        $"@{AcquisitionCompletionStateDetailsColumn}," +
											  $"@{IsModifiedAfterSubmissionColumn}) ";
		public BatchRun GetBatchRunById(IDbConnection connection, long batchRunId)
        {
            try
            {
	            var batchRun =
		            connection.Query<BatchRun>(
			            SelectSql +
			            $"WHERE {IdColumn} = {batchRunId}").FirstOrDefault();
				return batchRun;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetBatchRunById method", ex);
				throw;
			}
		}

		public BatchRun GetBatchRunByGuid(IDbConnection connection,
		    Guid batchRunGuid)
		{
			try
			{
				BatchRun batchRun = connection.Query<BatchRun>(
					SelectSql +
					$"WHERE {TableName}.{GuidColumn} = '{batchRunGuid}'").FirstOrDefault();
				return batchRun;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetBatchRunByName method", ex);
				throw;
			}
		}
        public BatchRun InsertBatchRun(IDbConnection connection,
	        Guid batchResultSetGuid,
	        Guid batchRunGuid,
            bool isBaselineRun, 
	        int repeatNumber,
            long sequenceSampleId,
			string sequenceSampleName,
	        AcquisitionMethod acquisitionMethod, 
	        ProcessingMethod processingMethod,
            DateTime acquistionStartTime, 
	        short dataSourceType,
	        StreamDataBatchResult[] streamDatas)
        {
            try
            {
                BatchRun batchRun = GetBatchRunByGuid(connection, batchRunGuid);
                BatchResultSetDao batchResultSetDao = new BatchResultSetDao();
                BatchResultSet batchResultSet = batchResultSetDao.GetBatchResultSetByGuid(connection, batchResultSetGuid);
                if (batchRun == null)
                {
                    batchRun = new BatchRun()
                    {
                        Name = sequenceSampleName,
                        AcquisitionCompletionState = 0,
                        BatchResultSetId = batchResultSet.Id,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = Guid.NewGuid().ToString(), // TODO
                        ModifiedDate = DateTime.UtcNow,
                        ModifiedUserId = Guid.NewGuid().ToString(), // TODO
                        SequenceSampleInfoBatchResultId = sequenceSampleId,
                        Guid = batchRunGuid,
                        AcquisitionMethodBatchResultId = acquisitionMethod.Id,
                        ProcessingMethodBatchResultId = processingMethod.Id,
                        IsBaselineRun = isBaselineRun,
                        RepeatIndex = repeatNumber,
						AcquisitionTime = acquistionStartTime,
						DataSourceType = dataSourceType
                    };
                    batchRun.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", batchRun);
                    Array.ForEach(streamDatas, c => c.BatchRunId = batchRun.Id);

                    // StreamData
                    StreamDataBatchResultDao streamDataBatchResultDao = new StreamDataBatchResultDao();
                    streamDataBatchResultDao.InsertStreamDatas(connection, streamDatas);
                }

                return batchRun;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertBatchRun method", ex);
                throw;
            }
        }

        public bool Create(IDbConnection connection, BatchRun batchRun)
        {
	        try
	        {
		        batchRun.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", batchRun);
		        return batchRun.Id != 0;
	        }
			catch (Exception ex)
	        {
		        Log.Error("Error in Create method", ex);
		        throw;
	        }
        }

		public (bool, BatchRun) EndBatchRun(IDbConnection connection,
	        Guid batchRunGuid, 
	        short acquisitionCompletionState, 
	        bool isModifiedAfterSubmission,
	        string acquisitionCompletionStateDetails)
		{
			try
			{
				BatchRun batchRun = connection.QueryFirstOrDefault<BatchRun>(
					SelectSql +
					$"WHERE {GuidColumn} = @{GuidColumn}",
					new {Guid = batchRunGuid});

				int rowsUpdated = 0;

				if (batchRun != null)
				{
					batchRun.ModifiedDate = DateTime.UtcNow;
					batchRun.ModifiedUserId = Guid.NewGuid().ToString(); // TODO
					batchRun.AcquisitionCompletionState = acquisitionCompletionState;

					rowsUpdated = connection.Execute(
						$"UPDATE {TableName} " +
						$"SET {AcquisitionCompletionStateColumn} = @{AcquisitionCompletionStateColumn}," +
						$"{ModifiedDateColumn} = @{ModifiedDateColumn}," +
						$"{ModifiedUserIdColumn} = @{ModifiedUserIdColumn}," +
						$"{IsModifiedAfterSubmissionColumn} = @{IsModifiedAfterSubmissionColumn}," +
						$"{AcquisitionCompletionStateDetailsColumn} = @{AcquisitionCompletionStateDetailsColumn} " +
						$"WHERE {GuidColumn} = @{GuidColumn}",
						new { AcquisitionCompletionState = acquisitionCompletionState,
							ModifiedDate = batchRun.ModifiedDate,
							ModifiedUserId = batchRun.ModifiedUserId,
							IsModifiedAfterSubmission = isModifiedAfterSubmission,
							AcquisitionCompletionStateDetails = acquisitionCompletionStateDetails,
							Guid = batchRunGuid});
				}

				return (rowsUpdated > 0, batchRun);
			}
			catch (Exception ex)
			{
				Log.Error("Error in EndBatchRun method", ex);
				throw;
			}
		}

	    public IList<BatchRun> GetBatchRunsByBatchResultSetId(IDbConnection connection, long batchResultSetId)
	    {
	        try
	        {
	            return
	                connection.Query<BatchRun>(
	                    SelectSql +
	                    $"WHERE {BatchResultSetIdColumn} = {batchResultSetId} " +
	                    $"ORDER BY {IdColumn} ASC").ToList();
            }
	        catch (Exception ex)
	        {
	            Log.Error("Error in GetBatchRunsByBatchResultSetId method", ex);
	            throw;
            }
	    }

	    public long? GetBatchRunIdByGuid(IDbConnection connection, Guid batchRunGuid)
	    {
	        try
	        {
	            return connection.QueryFirstOrDefault<long?>(
	                $"SELECT {TableName}.{IdColumn} " +
	                $"FROM {TableName} " +
	                $"WHERE {GuidColumn} = '{batchRunGuid}'");
	        }
	        catch (Exception ex)
	        {
	            Log.Error($"Error in GetBatchRunIdByGuid method", ex);
	            throw;
	        }
	    }

	    public BatchRun GetBatchRunInfo(IDbConnection connection, Guid batchRunGuid)
	    {
	        try
	        {
	            var batchRun = connection.QuerySingleOrDefault<BatchRun>(
	                SelectSql +
	                $"WHERE {GuidColumn} = '{batchRunGuid}'");
	            return batchRun;
	        }
	        catch (Exception ex)
	        {
	            Log.Error("Error in GetBatchRunInfo method", ex);
	            throw;
	        }
	    }

	    public BatchRun[] GetBatchRunsByBatchResultSetIdAndSequenceSampleInfoBatchResultId(IDbConnection connection, long batchResultSetId)
	    {
		    try
		    {
			    return
				    connection.Query<BatchRun>(
					    SelectSql +
					    $"WHERE {BatchResultSetIdColumn} = {batchResultSetId}").ToArray();
		    }
		    catch (Exception ex)
		    {
			    Log.Error("Error in GetBatchRunsByBatchResultSetId method", ex);
			    throw;
		    }
	    }
	}
}
