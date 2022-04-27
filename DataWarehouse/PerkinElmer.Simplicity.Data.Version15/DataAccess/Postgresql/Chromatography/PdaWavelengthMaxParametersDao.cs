using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class PdaWavelengthMaxParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaWavelengthMaxParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string MinWavelengthColumn { get; } = "MinWavelength";
		internal static string MaxWavelengthColumn { get; } = "MaxWavelength";
		internal static string ApplyBaselineCorrectionColumn { get; } = "ApplyBaselineCorrection";
		internal static string UseAutoAbsorbanceThresholdColumn { get; } = "UseAutoAbsorbanceThreshold";
		internal static string ManualAbsorbanceThresholdColumn { get; } = "ManualAbsorbanceThreshold";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{MinWavelengthColumn}," +
		                                      $"@{MaxWavelengthColumn}," +
		                                      $"@{ApplyBaselineCorrectionColumn}," +
		                                      $"@{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"@{ManualAbsorbanceThresholdColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{ApplyBaselineCorrectionColumn}," +
		                                      $"{UseAutoAbsorbanceThresholdColumn}," +
		                                      $"{ManualAbsorbanceThresholdColumn} ";
		public virtual void Create(IDbConnection connection, PdaWavelengthMaxParameters pdaWavelengthMaxParameters)
		{
			try
			{
				pdaWavelengthMaxParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaWavelengthMaxParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaWavelengthMaxParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaWavelengthMaxParameters>(
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
