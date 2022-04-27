using System;
using System.Data;
using Dapper;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class ProcessingMethodBatchResultDao : ProcessingMethodBaseDao
    {
		public ProcessingMethod GetProcessingMethod(IDbConnection connection, string projectName, string batchResultName, Guid processingMethodGuid)
		{
			try
			{
				projectName = projectName.ToLower();
				batchResultName = batchResultName.ToLower();

				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					selectSql +
					$"FROM {BatchResultSetDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {BatchResultSetDao.TableName}.{BatchResultSetDao.ProjectIdColumn} " +
					$"INNER JOIN {BatchResultSetToProcessingMethodMapDao.TableName} ON {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} = {BatchResultSetToProcessingMethodMapDao.TableName}.{BatchResultSetToProcessingMethodMapDao.BatchResultIdSetColumn} " +
					$"INNER JOIN {TableName} ON {BatchResultSetToProcessingMethodMapDao.TableName}.{BatchResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName " +
					$"AND LOWER({BatchResultSetDao.TableName}.{BatchResultSetDao.NameColumn}) = @BatchResultSetName " +
					$"AND {TableName}.{GuidColumn} = @ProcessingMethodGuid",
					new { ProjectName = projectName, BatchResultSetName = batchResultName, ProcessingMethodGuid = processingMethodGuid });

				return GetProcessingMethodChildren(connection, processingMethod);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}


		public ProcessingMethod GetProcessingMethodInfoForBatchResultSet(IDbConnection connection, long batchResultSetId, Guid processingMethodGuid)
        {
            try
            {
                var processingMethod =
				   connection.QueryFirstOrDefault<ProcessingMethod>(
					   selectSql +
					   $"FROM {BatchResultSetToProcessingMethodMapDao.TableName} " +
					   $"INNER JOIN {BatchResultSetDao.TableName} ON {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} = {BatchResultSetToProcessingMethodMapDao.TableName}.{BatchResultSetToProcessingMethodMapDao.BatchResultIdSetColumn} " +
					   $"INNER JOIN {TableName} ON {BatchResultSetToProcessingMethodMapDao.TableName}.{BatchResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					   $"WHERE {BatchResultSetDao.TableName}.{BatchResultSetDao.IdColumn} = '{batchResultSetId}' AND {TableName}.{GuidColumn} = '{processingMethodGuid}'");

			    return processingMethod;
			}
			catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodInfoForBatchResultSet method", ex);
                throw;
            }
        }

		public void Create(IDbConnection connection, long batchResultSetId, ProcessingMethod processingMethod)
		{
			try
			{
				Create(connection, processingMethod);

				var mapDao = new BatchResultSetToProcessingMethodMapDao();
				var map = new BatchResultSetToProcessingMethodMap()
				{
					BatchResultSetId = batchResultSetId,
					ProcessingMethodId = processingMethod.Id
				};

				mapDao.Create(connection, map);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

        public ProcessingMethod GetProcessingMethodInfoById(IDbConnection connection, long processingMethodId)
        {
            try
            {
                return connection.QueryFirstOrDefault<ProcessingMethod>(
	                selectSql +
	                $"FROM {TableName} " +
	                $"WHERE {IdColumn} = {processingMethodId}");

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodById method", ex);
                throw;
            }
        }
        public ProcessingMethod GetProcessingMethodById(IDbConnection connection, long processingMethodId)
        {
	        try
	        {
		        var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
			        selectSql +
			        $"FROM {TableName} " +
			        $"WHERE {IdColumn} = {processingMethodId}");
		        return GetProcessingMethodChildren(connection, processingMethod);
    }
	        catch (Exception ex)
	        {
		        Log.Error("Error in GetProcessingMethodById method", ex);
		        throw;
	        }
        }
        public bool Delete(IDbConnection connection, long processingMethodId)
        {
	        try
	        {
		        var cnt = connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = {processingMethodId}");
		        return cnt > 0;
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error occured in Delete method", ex);
		        throw;
	        }
        }
	}
}