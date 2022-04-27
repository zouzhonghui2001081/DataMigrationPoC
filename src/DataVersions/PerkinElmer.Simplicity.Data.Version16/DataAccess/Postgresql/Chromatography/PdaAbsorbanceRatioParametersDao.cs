using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class PdaAbsorbanceRatioParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaAbsorbanceRatioParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string WavelengthAColumn { get; } = "WavelengthA";
		internal static string WavelengthBColumn { get; } = "WavelengthB";
		internal static string ApplyBaselineCorrectionColumn { get; } = "ApplyBaselineCorrection";
		internal static string UseAutoAbsorbanceThresholdColumn { get; } = "UseAutoAbsorbanceThreshold";
		internal static string ManualAbsorbanceThresholdColumn { get; } = "ManualAbsorbanceThreshold";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{WavelengthAColumn}," +
		                                      $"{WavelengthBColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{WavelengthAColumn}," +
		                                      $"@{WavelengthBColumn}," +
		                                      $"@{ApplyBaselineCorrectionColumn}," +
		                                      $"@{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"@{ManualAbsorbanceThresholdColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{WavelengthAColumn}," +
		                                      $"{WavelengthBColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn} ";
		public virtual void Create(IDbConnection connection, PdaAbsorbanceRatioParameters pdaAbsorbanceRatioParameters)
		{
			try
			{
				pdaAbsorbanceRatioParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaAbsorbanceRatioParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaAbsorbanceRatioParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaAbsorbanceRatioParameters>(
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
