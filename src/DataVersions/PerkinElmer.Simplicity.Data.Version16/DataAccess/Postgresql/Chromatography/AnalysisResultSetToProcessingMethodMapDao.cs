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
	internal class AnalysisResultSetToProcessingMethodMapDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "AnalysisResultSetToProcessingMethodMap";
		internal static string IdColumn { get; } = "Id";
		internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";

		public void Create(IDbConnection connection, AnalysisResultSetToProcessingMethodMap map)
		{
			try
			{
				connection.Execute(
					$"INSERT INTO {TableName} ({AnalysisResultSetIdColumn}," +
					$"{ProcessingMethodIdColumn}) " +
					$"VALUES (@{AnalysisResultSetIdColumn}," +
					$"@{ProcessingMethodIdColumn})", map);
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateChildren method", ex);
				throw;
			}
		}

		public List<long> GetProcessingMethodIds(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				var processingMethodIds = connection.Query<long>($"SELECT {ProcessingMethodIdColumn} " +
				                                                 $"FROM {TableName} " +
				                                                 $"WHERE {AnalysisResultSetIdColumn}={analysisResultSetId}").ToList();

				return processingMethodIds;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodIds method", ex);
				throw;
			}
		}

		public void DeleteByAnalysisResultSetId(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} " +
				                   $"WHERE {AnalysisResultSetIdColumn}={analysisResultSetId}");

			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteByAnalysisResultSetId method", ex);
				throw;
			}
		}

	    public void DeleteByAnalysisResultSetProcessingMethodId(IDbConnection connection, long analysisResultSetId,
	        long processingMethodId)
	    {
	        try
	        {
	            connection.Execute($"DELETE FROM {TableName} " +
	                               $"WHERE {AnalysisResultSetIdColumn}={analysisResultSetId} AND {ProcessingMethodIdColumn}={processingMethodId}");

	        }
	        catch (Exception ex)
	        {
	            Log.Error("Error in DeleteByAnalysisResultSetProcessingMethodId method", ex);
	            throw;
	        }
        }
    }
}
