using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class PdaBaselineCorrectionParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaBaselineCorrectionParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string CorrectionTypeColumn { get; } = "CorrectionType";
		internal static string SelectedSpectrumTimeColumn { get; } = "SelectedSpectrumTime";
		internal static string RangeStartColumn { get; } = "RangeStart";
		internal static string RangeEndColumn { get; } = "RangeEnd";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ChannelMethodIdColumn}," +
		                                      $"{CorrectionTypeColumn}," +
		                                      $"{SelectedSpectrumTimeColumn}," +
		                                      $"{RangeStartColumn}," +
		                                      $"{RangeEndColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ChannelMethodIdColumn}," +
		                                      $"@{CorrectionTypeColumn}," +
		                                      $"@{SelectedSpectrumTimeColumn}," +
		                                      $"@{RangeStartColumn}," +
		                                      $"@{RangeEndColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ChannelMethodIdColumn}," +
		                                      $"{CorrectionTypeColumn}," +
		                                      $"{SelectedSpectrumTimeColumn}," +
		                                      $"{RangeStartColumn}," +
		                                      $"{RangeEndColumn} ";
		public virtual void Create(IDbConnection connection, PdaBaselineCorrectionParameters pdaBaselineCorrectionParameters)
		{
			try
			{
				pdaBaselineCorrectionParameters.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaBaselineCorrectionParameters);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public PdaBaselineCorrectionParameters GetByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<PdaBaselineCorrectionParameters>(
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
