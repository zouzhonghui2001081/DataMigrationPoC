using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class ProcessingMethodModifiableDao : ProcessingMethodBaseDao
	{
		public ProcessingMethod GetProcessingMethodById(IDbConnection connection, long processingMethodId)
		{
			try
			{
				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					selectSql +
						$"FROM {TableName} WHERE {IdColumn} = {processingMethodId}");

				return processingMethod;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodById method", ex);
				throw;
			}
		}
		public ProcessingMethod GetProcessingMethod(IDbConnection connection, long processingMethodId)
		{
			try
			{
				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					selectSql +
					$"FROM {TableName} WHERE {IdColumn} = {processingMethodId}");

				GetProcessingMethodChildren(connection, processingMethod);

				return processingMethod;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodById method", ex);
				throw;
			}
		}

		public List<ProcessingMethod> GetProcessingMethodsByAnalysisResultSetId(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				var processingMethods = connection.Query<ProcessingMethod>(
					selectSql +
					$"FROM {TableName} " +
					$"INNER JOIN {AnalysisResultSetToProcessingMethodMapDao.TableName} ON {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {AnalysisResultSetToProcessingMethodMapDao.AnalysisResultSetIdColumn} = {analysisResultSetId}").ToList();

				foreach (var processingMethod in processingMethods)
				{
					GetProcessingMethodChildren(connection, processingMethod);
				}

				return processingMethods;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodsByAnalysisResultSetId method", ex);
				throw;
			}
		}

	    public List<ProcessingMethod> GetProcessingMethodInfosByAnalysisResultSetId(IDbConnection connection, long analysisResultSetId)
	    {
	        try
	        {
	            var processingMethods = connection.Query<ProcessingMethod>(
	                selectSql +
	                $"FROM {TableName} " +
	                $"INNER JOIN {AnalysisResultSetToProcessingMethodMapDao.TableName} ON {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
	                $"WHERE {AnalysisResultSetToProcessingMethodMapDao.AnalysisResultSetIdColumn} = {analysisResultSetId}").ToList();
	            return processingMethods;
	        }
	        catch (Exception ex)
	        {
	            Log.Error("Error in GetProcessingMethodInfosByAnalysisResultSetId method", ex);
	            throw;
	        }
	    }

        //TODO: Rename method to CreateOrUpdate
	    public long Create(IDbConnection connection, long analysisResultSetId, ProcessingMethod processingMethod)
		{
			try
			{
				processingMethod.Id = connection.QueryFirstOrDefault<long>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {TableName} " +
					$"INNER JOIN {AnalysisResultSetToProcessingMethodMapDao.TableName} ON {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {AnalysisResultSetToProcessingMethodMapDao.AnalysisResultSetIdColumn} = {analysisResultSetId} AND {TableName}.{GuidColumn} = '{processingMethod.Guid}'");

				if (processingMethod.Id > 0)
				{
					// Update existing
					string sql =
						updateSql +
						$"WHERE {IdColumn} = {processingMethod.Id} RETURNING Id";
					connection.ExecuteScalar<long>(sql, processingMethod);

					UpdateChildren(connection, processingMethod);
				}
				else
				{
					// Create new
					Create(connection, processingMethod);

					var map = new AnalysisResultSetToProcessingMethodMap()
					{
						ProcessingMethodId = processingMethod.Id,
						AnalysisResultSetId = analysisResultSetId
					};

					var mapDao = new AnalysisResultSetToProcessingMethodMapDao();
					mapDao.Create(connection, map);
				}

				return processingMethod.Id;
			}
			catch (Exception exception)
			{
				Log.Error(
					$"Error occured in SaveChannelMethodModifiable() method of class{GetType().Name} - {exception.Message}");
				throw;
			}
		}

		public bool Delete(IDbConnection connection, long processingMethodModifiableId)
		{
			try
			{
				var cnt = connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = {processingMethodModifiableId}");
				return cnt > 0;
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in Delete method", ex);
				throw;
			}
		}

		public bool IsExists(IDbConnection connection, string projectName, string processingMethodName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(projectName))
				{
					throw new ArgumentException(nameof(projectName));
				}

				if (string.IsNullOrWhiteSpace(processingMethodName))
				{
					throw new ArgumentException(nameof(processingMethodName));
				}

				string sql = $"SELECT {TableName}.{IdColumn} " +
				             $"FROM {ProjectDao.TableName} " +
				             $"INNER JOIN {AnalysisResultSetDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} " +
				             $"INNER JOIN {AnalysisResultSetToProcessingMethodMapDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} = {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.AnalysisResultSetIdColumn} " +
				             $"INNER JOIN {TableName} ON {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
				             $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = LOWER(@ProjectName) AND LOWER({TableName}.{NameColumn}) = LOWER(@ProcessingMethodName)";

				long processingMethodModifiableId = connection.QueryFirstOrDefault<long>(sql, new { ProjectName = projectName, ProcessingMethodName = processingMethodName });

				return (processingMethodModifiableId > 0);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsExists method", ex);
				throw;
			}
		}

		public (bool IsExist,long ProcessingMethodId) IsExists(IDbConnection connection, long analysisResultSetId, Guid processingMethodGuid)
		{
			try
			{
				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {TableName} " +
					$"INNER JOIN {AnalysisResultSetToProcessingMethodMapDao.TableName} ON {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {AnalysisResultSetToProcessingMethodMapDao.AnalysisResultSetIdColumn} = @AnalysisResultSetId AND {TableName}.{GuidColumn} = @ProcessingMethodGuid",
					new { AnalysisResultSetId = analysisResultSetId, ProcessingMethodGuid = processingMethodGuid});

				if (processingMethod == null)
					return (false,0);

				return (true, processingMethod.Id);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsExists method", ex);
				throw;
			}
		}

		public long GetEmbeddedProcessingMethodId(IDbConnection connection, long analysisResultSetId, ProcessingMethod processingMethod)
        {
            long id = connection.QueryFirstOrDefault<long>(
                $"SELECT {TableName}.{IdColumn} " +
                $"FROM {TableName} " +
                $"INNER JOIN {AnalysisResultSetToProcessingMethodMapDao.TableName} ON {AnalysisResultSetToProcessingMethodMapDao.TableName}.{AnalysisResultSetToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                $"WHERE {AnalysisResultSetToProcessingMethodMapDao.AnalysisResultSetIdColumn} = {analysisResultSetId} AND {TableName}.{GuidColumn} = '{processingMethod.Guid}'");
            return id;
        }
    }
}
