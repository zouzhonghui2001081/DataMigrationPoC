using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class PdaApexOptimizedParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaApexOptimizedParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		internal static string MinWavelengthColumn { get; } = "MinWavelength";
		internal static string MaxWavelengthColumn { get; } = "MaxWavelength";
		internal static string WavelengthBandwidthColumn { get; } = "WavelengthBandwidth";
		internal static string UseReferenceColumn { get; } = "UseReference";
		internal static string ReferenceWavelengthColumn { get; } = "ReferenceWavelength";
		internal static string ReferenceWavelengthBandwidthColumn { get; } = "ReferenceWavelengthBandwidth";
		internal static string ApplyBaselineCorrectionColumn { get; } = "ApplyBaselineCorrection";
		internal static string UseAutoAbsorbanceThresholdColumn { get; } = "UseAutoAbsorbanceThreshold";
		internal static string ManualAbsorbanceThresholdColumn { get; } = "ManualAbsorbanceThreshold";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ProcessingMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{WavelengthBandwidthColumn}," +
		                                      $"{UseReferenceColumn}," +
		                                      $"{ReferenceWavelengthColumn}," +
		                                      $"{ReferenceWavelengthBandwidthColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ProcessingMethodIdColumn}," +
		                                      $"@{MinWavelengthColumn}," +
		                                      $"@{MaxWavelengthColumn}," +
		                                      $"@{WavelengthBandwidthColumn}," +
		                                      $"@{UseReferenceColumn}," +
		                                      $"@{ReferenceWavelengthColumn}," +
		                                      $"@{ReferenceWavelengthBandwidthColumn}," +
		                                      $"@{ApplyBaselineCorrectionColumn}," +
		                                      $"@{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"@{ManualAbsorbanceThresholdColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ProcessingMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{WavelengthBandwidthColumn}," +
		                                      $"{UseReferenceColumn}," +
		                                      $"{ReferenceWavelengthColumn}," +
		                                      $"{ReferenceWavelengthBandwidthColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn} ";
		public virtual void Create(IDbConnection connection, PdaApexOptimizedParameters pdaApexOptimizedParameters)
		{
			try
			{
				pdaApexOptimizedParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaApexOptimizedParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaApexOptimizedParameters GetByProcessingMethodId(IDbConnection connection, long processingMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaApexOptimizedParameters>(
					$"{SelectSql} " +
					$"FROM {TableName} " +
					$"WHERE {ProcessingMethodIdColumn} = {processingMethodId}");

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByProcessingMethodId method", ex);
				throw;
			}
		}

	    public void Delete(IDbConnection connection, long processingMethodId)
	    {
	        try
	        {
	            connection.Execute(
	                $"DELETE FROM {TableName} " +
	                $"WHERE {TableName}.{ProcessingMethodIdColumn} = {processingMethodId}");
	        }
	        catch (Exception ex)
	        {
	            Log.Error($"Error in Delete method", ex);
	            throw;
	        }
	    }
    }
}
