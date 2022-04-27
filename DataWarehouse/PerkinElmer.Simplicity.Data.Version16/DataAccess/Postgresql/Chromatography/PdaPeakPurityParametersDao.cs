using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class PdaPeakPurityParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaPeakPurityParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string MinWavelengthColumn { get; } = "MinWavelength";
		internal static string MaxWavelengthColumn { get; } = "MaxWavelength";
		internal static string MinimumDataPointsColumn { get; } = "MinimumDataPoints";
		internal static string ApplyBaselineCorrectionColumn { get; } = "ApplyBaselineCorrection";
		internal static string PurityLimitColumn { get; } = "PurityLimit";
		internal static string PercentOfPeakHeightForSpectraColumn { get; } = "PercentOfPeakHeightForSpectra";
		internal static string UseAutoAbsorbanceThresholdColumn { get; } = "UseAutoAbsorbanceThreshold";
		internal static string ManualAbsorbanceThresholdColumn { get; } = "ManualAbsorbanceThreshold";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{MinimumDataPointsColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{PurityLimitColumn}," +
		                                      $"{PercentOfPeakHeightForSpectraColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn})" +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{MinWavelengthColumn}," +
		                                      $"@{MaxWavelengthColumn}," +
		                                      $"@{MinimumDataPointsColumn}," +
		                                      $"@{ApplyBaselineCorrectionColumn}," +
		                                      $"@{PurityLimitColumn}," +
		                                      $"@{PercentOfPeakHeightForSpectraColumn}," +
		                                      $"@{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"@{ManualAbsorbanceThresholdColumn}) ";
		
		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{MinimumDataPointsColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{PurityLimitColumn}," +
		                                      $"{PercentOfPeakHeightForSpectraColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn} ";
		public virtual void Create(IDbConnection connection, PdaPeakPurityParameters pdaPeakPurityParameters)
		{
			try
			{
				pdaPeakPurityParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaPeakPurityParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaPeakPurityParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaPeakPurityParameters>(
					$"{SelectSql} " +
					$"FROM {TableName} " +
					$"WHERE {ChannelMethodIdColumn} = {channelMethodId}");

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByChannelMethodId method", ex);
				throw;
			}
		}
	}
}
