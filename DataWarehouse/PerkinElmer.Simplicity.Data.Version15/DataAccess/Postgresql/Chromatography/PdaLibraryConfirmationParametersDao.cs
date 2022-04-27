using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class PdaLibraryConfirmationParametersDao
    {
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaLibraryConfirmationParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string MinWavelengthColumn { get; } = "MinWavelength";
		internal static string MaxWavelengthColumn { get; } = "MaxWavelength";
		internal static string BaselineCorrectionEnabledColumn { get; } = "BaselineCorrectionEnabled";
		internal static string HitDistanceThresholdColumn { get; } = "HitDistanceThreshold";
		

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{BaselineCorrectionEnabledColumn}," +
		                                      $"{HitDistanceThresholdColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{MinWavelengthColumn}," +
		                                      $"@{MaxWavelengthColumn}," +
		                                      $"@{BaselineCorrectionEnabledColumn}," +
		                                      $"@{HitDistanceThresholdColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{MinWavelengthColumn}," +
		                                      $"{MaxWavelengthColumn}," +
		                                      $"{BaselineCorrectionEnabledColumn}," +
		                                      $"{HitDistanceThresholdColumn} ";
		public virtual void Create(IDbConnection connection, PdaLibraryConfirmationParameters pdaLibraryConfirmationParameters)
		{
			try
			{
                pdaLibraryConfirmationParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaLibraryConfirmationParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaLibraryConfirmationParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaLibraryConfirmationParameters>(
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
