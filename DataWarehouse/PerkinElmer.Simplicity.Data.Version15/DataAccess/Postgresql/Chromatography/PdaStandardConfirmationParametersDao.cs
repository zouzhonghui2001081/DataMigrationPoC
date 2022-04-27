using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class PdaStandardConfirmationParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaStandardConfirmationParameters";
		internal static string IdColumn { get; } = "Id";
	    internal static string PdaStandardConfirmationGuidColumn { get; } = "PdaStandardConfirmationGuid";
        internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string MinWavelengthColumn { get; } = "MinWavelength";
		internal static string MaxWavelengthColumn { get; } = "MaxWavelength";
		internal static string MinimumDataPointsColumn { get; } = "MinimumDataPoints";
		internal static string PassThresholdColumn { get; } = "PassThreshold";
		internal static string StandardTypeColumn { get; } = "StandardType";
		internal static string ApplyBaselineCorrectionColumn { get; } = "ApplyBaselineCorrection";
		internal static string UseAutoAbsorbanceThresholdForSampleColumn { get; } = "UseAutoAbsorbanceThresholdForSample";
		internal static string ManualAbsorbanceThresholdForSampleColumn { get; } = "ManualAbsorbanceThresholdForSample";
		internal static string UseAutoAbsorbanceThresholdForStandardColumn { get; } = "UseAutoAbsorbanceThresholdForStandard";
		internal static string ManualAbsorbanceThresholdForStandardColumn { get; } = "ManualAbsorbanceThresholdForStandard";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{PdaStandardConfirmationGuidColumn}," +
		                                      $"{StandardTypeColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{MinimumDataPointsColumn}," +
		                                      $"{PassThresholdColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdForSampleColumn}," +
		                                      $"{ManualAbsorbanceThresholdForSampleColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdForStandardColumn}," +
		                                      $"{ManualAbsorbanceThresholdForStandardColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{PdaStandardConfirmationGuidColumn}," +
		                                      $"@{StandardTypeColumn}," +
		                                      $"@{MinWavelengthColumn}," +
		                                      $"@{MaxWavelengthColumn}," +
		                                      $"@{MinimumDataPointsColumn}," +
		                                      $"@{PassThresholdColumn}," +
		                                      $"@{ApplyBaselineCorrectionColumn}," +
		                                      $"@{UseAutoAbsorbanceThresholdForSampleColumn}," +
		                                      $"@{ManualAbsorbanceThresholdForSampleColumn}," +
		                                      $"@{UseAutoAbsorbanceThresholdForStandardColumn}," +
		                                      $"@{ManualAbsorbanceThresholdForStandardColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{PdaStandardConfirmationGuidColumn}," +
		                                      $"{StandardTypeColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{MinimumDataPointsColumn}," +
		                                      $"{PassThresholdColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdForSampleColumn}," +
		                                      $"{ManualAbsorbanceThresholdForSampleColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdForStandardColumn}," +
		                                      $"{ManualAbsorbanceThresholdForStandardColumn} ";
		public virtual void Create(IDbConnection connection, PdaStandardConfirmationParameters pdaStandardConfirmationParameters)
		{
			try
			{
				pdaStandardConfirmationParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaStandardConfirmationParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaStandardConfirmationParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaStandardConfirmationParameters>(
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
