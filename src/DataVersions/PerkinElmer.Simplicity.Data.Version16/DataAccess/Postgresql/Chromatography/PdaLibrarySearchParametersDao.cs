using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class PdaLibrarySearchParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaLibrarySearchParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string MinWavelengthColumn { get; } = "MinWavelength";
		internal static string MaxWavelengthColumn { get; } = "MaxWavelength";
		internal static string MatchRetentionTimeWindowColumn { get; } = "MatchRetentionTimeWindow";
		internal static string MatchRetentionTimeWindowEnabledColumn { get; } = "MatchRetentionTimeWindowEnabled";
		internal static string BaselineCorrectionEnabledColumn { get; } = "BaselineCorrectionEnabled";
		internal static string HitDistanceThresholdColumn { get; } = "HitDistanceThreshold";
		internal static string PeakLibrarySearchColumn { get; } = "PeakLibrarySearch";
		internal static string UseWavelengthLimitsColumn { get; } = "UseWavelengthLimits";
		internal static string MaxNumberOfResultsColumn { get; } = "MaxNumberOfResults";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{MatchRetentionTimeWindowColumn}," +
		                                      $"{MatchRetentionTimeWindowEnabledColumn}," +
		                                      $"{BaselineCorrectionEnabledColumn}," +
		                                      $"{HitDistanceThresholdColumn}," +
		                                      $"{MaxNumberOfResultsColumn}," +
		                                      $"{PeakLibrarySearchColumn}," +
		                                      $"{UseWavelengthLimitsColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{MinWavelengthColumn}," +
		                                      $"@{MaxWavelengthColumn}," +
		                                      $"@{MatchRetentionTimeWindowColumn}," +
		                                      $"@{MatchRetentionTimeWindowEnabledColumn}," +
		                                      $"@{BaselineCorrectionEnabledColumn}," +
		                                      $"@{HitDistanceThresholdColumn}," +
		                                      $"@{MaxNumberOfResultsColumn}," +
		                                      $"@{PeakLibrarySearchColumn}," +
											  $"@{UseWavelengthLimitsColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{MatchRetentionTimeWindowColumn}," +
		                                      $"{MatchRetentionTimeWindowEnabledColumn}," +
		                                      $"{BaselineCorrectionEnabledColumn}," +
		                                      $"{HitDistanceThresholdColumn}," +
		                                      $"{MaxNumberOfResultsColumn}," +
		                                      $"{PeakLibrarySearchColumn}," +
		                                      $"{UseWavelengthLimitsColumn} ";
		public virtual void Create(IDbConnection connection, PdaLibrarySearchParameters pdaLibrarySearchParameters)
		{
			try
			{
				pdaLibrarySearchParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaLibrarySearchParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaLibrarySearchParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaLibrarySearchParameters>(
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
