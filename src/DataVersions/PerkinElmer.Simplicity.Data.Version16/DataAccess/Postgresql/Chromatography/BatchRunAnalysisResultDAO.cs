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
    internal class BatchRunAnalysisResultDao
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static string IdColumn { get; } = "Id";
        internal static string ProjectIdColumn { get; } = "ProjectId";
        internal static string TableName { get; } = "BatchRunAnalysisResult";
        internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
   
        //Original Sequence Guid and Batch Run Meta Data
        internal static string OriginalBatchResultSetGuidColumn { get; } = "OriginalBatchResultSetGuid";
        internal static string OriginalBatchRunInfoGuidColumn { get; } = "OriginalBatchRunInfoGuid";
        internal static string ModifiableBatchRunInfoGuidColumn { get; } = "ModifiableBatchRunInfoGuid";
        internal static string BatchRunNameColumn { get; } = "BatchRunName";
        internal static string BatchRunCreatedDateColumn { get; } = "BatchRunCreatedDate";
        internal static string BatchRunCreatedUserIdColumn { get; } = "BatchRunCreatedUserId";
        internal static string BatchRunModifiedDateColumn { get; } = "BatchRunModifiedDate";
        internal static string BatchRunModifiedUserIdColumn { get; } = "BatchRunModifiedUserId";
        internal static string SequenceSampleInfoModifiableIdColumn { get; } = "SequenceSampleInfoModifiableId";
        internal static string ProcessingMethodModifiableIdColumn { get; } = "ProcessingMethodModifiableId";
        internal static string CalibrationMethodModifiableIdColumn { get; } = "CalibrationMethodModifiableId";
        internal static string IsBlankSubtractorColumn { get; } = "IsBlankSubtractor";
        internal static string DataSourceTypeColumn { get; } = "DataSourceType";

		protected readonly string InsertSql =
		   $"INSERT INTO {TableName} " +
		   $"({ProjectIdColumn}," +
		   $"{AnalysisResultSetIdColumn}," +
		   $"{OriginalBatchResultSetGuidColumn}," +
		   $"{OriginalBatchRunInfoGuidColumn}," +
		   $"{ModifiableBatchRunInfoGuidColumn}," +
		   $"{BatchRunNameColumn}," +
		   $"{BatchRunCreatedDateColumn}," +
		   $"{BatchRunCreatedUserIdColumn}," +
		   $"{BatchRunModifiedDateColumn}," +
		   $"{BatchRunModifiedUserIdColumn}," +
		   $"{ProcessingMethodModifiableIdColumn}," +
		   $"{CalibrationMethodModifiableIdColumn}," +
		   $"{IsBlankSubtractorColumn}," +
		   $"{SequenceSampleInfoModifiableIdColumn}," +
		   $"{DataSourceTypeColumn}) " +
		   "VALUES " +
		   $"(@{ProjectIdColumn}," +
		   $"@{AnalysisResultSetIdColumn}," +
		   $"@{OriginalBatchResultSetGuidColumn}," +
		   $"@{OriginalBatchRunInfoGuidColumn}," +
		   $"@{ModifiableBatchRunInfoGuidColumn}," +
		   $"@{BatchRunNameColumn}," +
		   $"@{BatchRunCreatedDateColumn}," +
		   $"@{BatchRunCreatedUserIdColumn}," +
		   $"@{BatchRunModifiedDateColumn}," +
		   $"@{BatchRunModifiedUserIdColumn}," +
		   $"@{ProcessingMethodModifiableIdColumn}," +
		   $"@{CalibrationMethodModifiableIdColumn}," +
		   $"@{IsBlankSubtractorColumn}," +
		   $"@{SequenceSampleInfoModifiableIdColumn}," +
		   $"@{DataSourceTypeColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {TableName}.{IdColumn}," +
			$"{TableName}.{ProjectIdColumn}," +
			$"{TableName}.{AnalysisResultSetIdColumn}," +
			$"{TableName}.{OriginalBatchResultSetGuidColumn}," +
			$"{TableName}.{OriginalBatchRunInfoGuidColumn}," +
			$"{TableName}.{ModifiableBatchRunInfoGuidColumn}," +
			$"{TableName}.{BatchRunNameColumn}," +
			$"{TableName}.{BatchRunCreatedDateColumn}," +
			$"{TableName}.{BatchRunCreatedUserIdColumn}," +
			$"{TableName}.{BatchRunModifiedDateColumn}," +
			$"{TableName}.{BatchRunModifiedUserIdColumn}," +
			$"{TableName}.{ProcessingMethodModifiableIdColumn}," +
			$"{TableName}.{CalibrationMethodModifiableIdColumn}," +
			$"{TableName}.{IsBlankSubtractorColumn}," +
			$"{TableName}.{SequenceSampleInfoModifiableIdColumn}," +
			$"{TableName}.{DataSourceTypeColumn} FROM {TableName} ";

		public IList<BatchRunAnalysisResult> GetBatchRunAnalysisResults(IDbConnection connection, long analysisResultSetId)
        {
            try
            {
				string query = $"SELECT {TableName}.{IdColumn} AS {IdColumn}," +
							   $"{AnalysisResultSetIdColumn}," +
							   $"{OriginalBatchResultSetGuidColumn}," +
							   $"{OriginalBatchRunInfoGuidColumn}," +
							   $"{ModifiableBatchRunInfoGuidColumn}," +
							   $"{BatchRunNameColumn}," +
							   $"{BatchRunCreatedDateColumn}," +
							   $"{BatchRunCreatedUserIdColumn}," +
							   $"{BatchRunModifiedDateColumn}," +
							   $"{BatchRunModifiedUserIdColumn}," +
							   $"{ProcessingMethodModifiableIdColumn}," +
							   $"{CalibrationMethodModifiableIdColumn}," +
							   $"{SequenceSampleInfoModifiableIdColumn}," +
							   $"{DataSourceTypeColumn} " +
							   $"FROM {TableName} " +
							   $"WHERE {TableName}.{AnalysisResultSetIdColumn} = '{analysisResultSetId}' AND {IsBlankSubtractorColumn} <> {true} " +
							   $"ORDER BY {IdColumn}";

				var batchRunAnalysisResults = connection.Query<BatchRunAnalysisResult>(query).ToList();
		
				// Assign BatchRun.Id
				var batchRunDao = new BatchRunDao();
				foreach (var batchRunAnalysisResult in batchRunAnalysisResults)
				{
					var batchRun =
						batchRunDao.GetBatchRunByGuid(connection, batchRunAnalysisResult.OriginalBatchRunInfoGuid);

					if (batchRun != null)
					{
						batchRunAnalysisResult.BatchRunId = batchRun.Id;
					}
				}

				return batchRunAnalysisResults;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetBatchRunAnalysisResults method", ex);
                throw;
            }
        }

		public IList<BatchRunAnalysisResult> GetBatchRunAnalysisResultsWithBlankSubtraction(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				string query = $"SELECT {TableName}.{IdColumn} AS {IdColumn}," +
				               $"{AnalysisResultSetIdColumn}," +
				               $"{OriginalBatchResultSetGuidColumn}," +
				               $"{OriginalBatchRunInfoGuidColumn}," +
				               $"{ModifiableBatchRunInfoGuidColumn}," +
				               $"{BatchRunNameColumn}," +
				               $"{BatchRunCreatedDateColumn}," +
				               $"{BatchRunCreatedUserIdColumn}," +
				               $"{BatchRunModifiedDateColumn}," +
				               $"{BatchRunModifiedUserIdColumn}," +
				               $"{ProcessingMethodModifiableIdColumn}," +
				               $"{CalibrationMethodModifiableIdColumn}," +
				               $"{IsBlankSubtractorColumn}," +
				               $"{SequenceSampleInfoModifiableIdColumn}," +
				               $"{DataSourceTypeColumn} " +
				               $"FROM {TableName} " +
				               $"WHERE {TableName}.{AnalysisResultSetIdColumn} = '{analysisResultSetId}' AND {IsBlankSubtractorColumn} ={true} " +
				               $"ORDER BY {IdColumn}";

				var batchRunAnalysisResults = connection.Query<BatchRunAnalysisResult>(query).ToList();

				// Assign BatchRun.Id
				var batchRunDao = new BatchRunDao();
				foreach (var batchRunAnalysisResult in batchRunAnalysisResults)
				{
					var batchRun =
						batchRunDao.GetBatchRunByGuid(connection, batchRunAnalysisResult.OriginalBatchRunInfoGuid);

					if (batchRun != null)
					{
						batchRunAnalysisResult.BatchRunId = batchRun.Id;
					}
				}

				return batchRunAnalysisResults;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetBatchRunAnalysisResultsWithBlankSubtraction method", ex);
				throw;
			}
		}

		public long SaveBatchRunAnalysisResult(IDbConnection connection,
			Guid projectGuid,
			BatchRunAnalysisResult batchRunAnalysisResultEntity)
		{
			try
			{
				long batchRunAnalysisResultId = 0;
				var projectDao = new ProjectDao();
				var project= projectDao.GetProject(connection, projectGuid);

				if (project != null)
				{
					batchRunAnalysisResultEntity.ProjectId = project.Id;
                        batchRunAnalysisResultId = connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", batchRunAnalysisResultEntity);
				}

				return batchRunAnalysisResultId;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public long SaveBatchRunAnalysisResult(IDbConnection connection, BatchRunAnalysisResult batchRunAnalysisResultEntity, string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentException("Project Name is empty");
            }
            long? projectId = ProjectDao.GetProjectId(connection, projectName);
            long batchRunAnalysisResultId = 0;
            try
            {
                if (batchRunAnalysisResultEntity != null && projectId.HasValue)
                {
                    batchRunAnalysisResultEntity.ProjectId = projectId.Value;
                    long existingbatchRunAnalysisResultId =
                        connection.QueryFirstOrDefault<long>(
                            $"SELECT {IdColumn} " +
                            $"FROM {TableName} " +
							$"WHERE {OriginalBatchRunInfoGuidColumn} = @OriginalBatchRunInfoGuid AND " +
                            $"{AnalysisResultSetIdColumn} = @AnalysisResultSetId AND " +
                            $"{ProjectIdColumn} = @ProjectId AND " +
                            $"{BatchRunNameColumn} = @BatchRunName AND " +
                            $"{IsBlankSubtractorColumn} = @IsBlankSubtractor",
							new
							{
								OriginalBatchRunInfoGuid = batchRunAnalysisResultEntity.OriginalBatchRunInfoGuid,
								AnalysisResultSetId = batchRunAnalysisResultEntity.AnalysisResultSetId,
								ProjectId = batchRunAnalysisResultEntity.ProjectId,
								BatchRunName = batchRunAnalysisResultEntity.BatchRunName,
								IsBlankSubtractor = batchRunAnalysisResultEntity.IsBlankSubtractor
							}
						);
                    if (existingbatchRunAnalysisResultId <= 0)
                    {
                        batchRunAnalysisResultId = connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", batchRunAnalysisResultEntity);
                    }
                    else
                    {
                        //batchRunAnalysisResultId = existingbatchRunAnalysisResultId;
                        string sql = $"Update {TableName} set {SequenceSampleInfoModifiableIdColumn} = {batchRunAnalysisResultEntity.SequenceSampleInfoModifiableId},{ProcessingMethodModifiableIdColumn} = {batchRunAnalysisResultEntity.ProcessingMethodModifiableId}, {CalibrationMethodModifiableIdColumn} = {batchRunAnalysisResultEntity.CalibrationMethodModifiableId} where {IdColumn} = {existingbatchRunAnalysisResultId} RETURNING Id";
                        batchRunAnalysisResultId = connection.ExecuteScalar<long>(sql, batchRunAnalysisResultEntity);
                    }

                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in SaveBatchRunAnalysisResult method", ex);
                throw;
            }
            return batchRunAnalysisResultId;
        }

      

        public long GetBatchRunAnalysisResultIdByBatchRunGuid(IDbConnection connection, long analysisResultSetId, Guid batchRunGuid)
        {

            string sql = $"Select {TableName}.{IdColumn} from {TableName} inner join {AnalysisResultSetDao.TableName} On " +
                         $"{TableName}.{AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{IdColumn}" +
                         $" where {OriginalBatchRunInfoGuidColumn} = @BatchRunGuid AND {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} = @AnalysisResultSetId";
            long batchRunAnalysisResultId =
                connection.QueryFirstOrDefault<long>(sql, new { BatchRunGuid = batchRunGuid, AnalysisResultSetId = analysisResultSetId});

            return batchRunAnalysisResultId;
        }

        public BatchRunAnalysisResult GetBatchRunAnalysisResult(IDbConnection connection, string projectName,
	        string analysisResultSetName, Guid modifiableBatchRunGuid)
        {
	        projectName = projectName.Replace("'", "''").ToLower();
	        analysisResultSetName = analysisResultSetName.Replace("'", "''").ToLower();

	        var select =
		        $"SELECT {TableName}.{IdColumn}," +
		        $"{TableName}.{AnalysisResultSetIdColumn}," +
		        $"{TableName}.{OriginalBatchResultSetGuidColumn}," +
		        $"{TableName}.{OriginalBatchRunInfoGuidColumn}," +
		        $"{TableName}.{ModifiableBatchRunInfoGuidColumn}," +
		        $"{TableName}.{BatchRunNameColumn}," +
		        $"{TableName}.{BatchRunCreatedDateColumn}," +
		        $"{TableName}.{BatchRunCreatedUserIdColumn}," +
		        $"{TableName}.{BatchRunModifiedDateColumn}," +
		        $"{TableName}.{BatchRunModifiedUserIdColumn}," +
		        $"{TableName}.{ProcessingMethodModifiableIdColumn}," +
		        $"{TableName}.{CalibrationMethodModifiableIdColumn}," +
		        $"{TableName}.{IsBlankSubtractorColumn}," +
		        $"{TableName}.{SequenceSampleInfoModifiableIdColumn}," +
		        $"{TableName}.{DataSourceTypeColumn} " +
		        $"FROM {ProjectDao.TableName} " +
		        $"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
		        $"INNER JOIN {TableName} ON {TableName}.{AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
		        "WHERE " +
		        $"LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND " +
		        $"LOWER({AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.NameColumn}) = @AnalysisResultSetName AND " +
		        $"{TableName}.{ModifiableBatchRunInfoGuidColumn} = @ModifiableBatchRunInfoGuid";

			return connection.QueryFirstOrDefault<BatchRunAnalysisResult>(select, new { ProjectName = projectName,
				AnalysisResultSetName = analysisResultSetName,
				ModifiableBatchRunInfoGuid = modifiableBatchRunGuid});
        }

        public bool RemoveBatchRunAnalysisResult(IDbConnection connection, long batchRunAnalysisResultId)
        {
            try
            {
                connection.Execute($"Delete from {TableName} where {IdColumn} = {batchRunAnalysisResultId}");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error in RemoveBatchRunAnalysisResult method", ex);
                throw;
            }
        }

        public long GetSequenceSampleInfoModifiableIdByBatchRunAnalysisResultSetId(IDbConnection connection, long batchRunAnalysisResultSetId)
        {
            try
            {
                string sql =
                    $"SELECT {TableName}.{IdColumn}," +
                    $"{TableName}.{ProjectIdColumn}," +
                    $"{TableName}.{OriginalBatchRunInfoGuidColumn}," + // Why is this repeated?
                    $"{TableName}.{AnalysisResultSetIdColumn}," +
                    $"{TableName}.{OriginalBatchResultSetGuidColumn}," +
                    $"{TableName}.{OriginalBatchRunInfoGuidColumn}," +  // Why is this repeated? 
					$"{TableName}.{ModifiableBatchRunInfoGuidColumn}," +
                    $"{TableName}.{BatchRunNameColumn}," +
                    $"{TableName}.{BatchRunCreatedDateColumn}," +
                    $"{TableName}.{BatchRunCreatedUserIdColumn}" +
                    $"{TableName}.{BatchRunModifiedDateColumn}," +
                    $"{TableName}.{BatchRunModifiedUserIdColumn}," +
                    $"{TableName}.{SequenceSampleInfoModifiableIdColumn}," +
                    $"{TableName}.{DataSourceTypeColumn}" +
                    $"FROM {TableName} where {IdColumn} = {batchRunAnalysisResultSetId}";

                long sequenceSampleInfoModifiableId = connection.QueryFirstOrDefault<long>(sql);
                return sequenceSampleInfoModifiableId;
            }
            catch (Exception ex)
            {
                Log.Error(
                    $"Error occured in GetSequenceSampleInfoModifiableIdByBatchRunAnalysisResultSetId() method of class{GetType().Name} - {ex.Message}");
                throw;
            }
        }

        public BatchRunAnalysisResult GetBatchRunAnalysisResultByModifiableBatchRunGuid(IDbConnection connection, long analysisResultSetId, Guid modifiableBatchRunGuid)
        {
            try
            {
                return  connection.QueryFirstOrDefault<BatchRunAnalysisResult>(
                        $"SELECT {TableName}.{IdColumn} AS {IdColumn}," +
                        $"{BatchRunDao.TableName}.{BatchRunDao.IdColumn} AS BatchRunId," +
                        $"{AnalysisResultSetIdColumn}," +
                        $"{BatchRunDao.SequenceSampleInfoBatchResultIdColumn}," +
                        $"{OriginalBatchResultSetGuidColumn}," +
                        $"{OriginalBatchRunInfoGuidColumn}," +
                        $"{ModifiableBatchRunInfoGuidColumn}," +
                        $"{BatchRunNameColumn}," +
                        $"{BatchRunCreatedDateColumn}," +
                        $"{BatchRunCreatedUserIdColumn}," +
                        $"{BatchRunModifiedDateColumn}," +
                        $"{BatchRunModifiedUserIdColumn}," +
                        $"{ProcessingMethodModifiableIdColumn}," +
                        $"{CalibrationMethodModifiableIdColumn}," +
                        $"{SequenceSampleInfoModifiableIdColumn}," +
                        $"{TableName}.{DataSourceTypeColumn} " +
                        $"FROM {TableName} " +
                        $"LEFT JOIN {BatchRunDao.TableName} ON {BatchRunDao.TableName}.{BatchRunDao.GuidColumn} = {TableName}.{OriginalBatchRunInfoGuidColumn} " +
                        $"WHERE {TableName}.{AnalysisResultSetIdColumn} = '{analysisResultSetId}' AND {TableName}.{ModifiableBatchRunInfoGuidColumn} = '{modifiableBatchRunGuid}'");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetBatchRunAnalysisResultByModifiableBatchRunGuid method", ex);
                throw;
            }
        }
    }
}
