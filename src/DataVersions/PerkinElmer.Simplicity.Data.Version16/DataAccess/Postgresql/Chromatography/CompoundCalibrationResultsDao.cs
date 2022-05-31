using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class CompoundCalibrationResultsDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "CompoundCalibrationResults";
		internal static string IdColumn { get; } = "Id";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		internal static string NotEnoughLevelsFoundErrorColumn { get; } = "NotEnoughLevelsFoundError";
		internal static string InvalidAmountErrorColumn { get; } = "InvalidAmountError";
		internal static string RegressionTypeColumn { get; } = "RegressionType";
		internal static string CoefficientsColumn { get; } = "Coefficients";
		internal static string RSquareColumn { get; } = "RSquare";
		internal static string RelativeStandardErrorValueColumn { get; } = "RelativeStandardErrorValue";
		internal static string GuidColumn { get; } = "Guid";
		internal static string NameColumn { get; } = "Name";
		internal static string ChannelIndexColumn { get; } = "ChannelIndex";
		internal static string ConfLimitTestResultColumn { get; } = "ConfLimitTestResult";
		internal static string RelativeStandardDeviationPercentColumn { get; } = "RelativeStandardDeviationPercent";
		internal static string CorrelationCoefficientColumn { get; } = "CorrelationCoefficient";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ProcessingMethodIdColumn}," +
		                                      $"{NotEnoughLevelsFoundErrorColumn}," +
		                                      $"{InvalidAmountErrorColumn}," +
		                                      $"{RegressionTypeColumn}," +
		                                      $"{RSquareColumn}," +
		                                      $"{RelativeStandardErrorValueColumn}," +
		                                      $"{GuidColumn}," +
		                                      $"{NameColumn}," +
		                                      $"{ChannelIndexColumn}," +
		                                      $"{ConfLimitTestResultColumn}," +
		                                      $"{RelativeStandardDeviationPercentColumn}," +
		                                      $"{CorrelationCoefficientColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ProcessingMethodIdColumn}," +
		                                      $"@{NotEnoughLevelsFoundErrorColumn}," +
		                                      $"@{InvalidAmountErrorColumn}," +
		                                      $"@{RegressionTypeColumn}," +
		                                      $"@{RSquareColumn}," +
		                                      $"@{RelativeStandardErrorValueColumn}," +
		                                      $"@{GuidColumn}," +
		                                      $"@{NameColumn}," +
		                                      $"@{ChannelIndexColumn}," +
		                                      $"@{ConfLimitTestResultColumn}," +
		                                      $"@{RelativeStandardDeviationPercentColumn}," +
		                                      $"@{CorrelationCoefficientColumn}) ";

		protected readonly string SelectSql =
			"SELECT " +
			$"{IdColumn}," +
			$"{ProcessingMethodIdColumn}," +
			$"{NotEnoughLevelsFoundErrorColumn}," +
			$"{InvalidAmountErrorColumn}," +
			$"{RegressionTypeColumn}," +
			$"{RSquareColumn}," +
			$"{RelativeStandardErrorValueColumn}," +
			$"{GuidColumn}," +
			$"{NameColumn}," +
			$"{ChannelIndexColumn}," +
			$"{ConfLimitTestResultColumn}," +
			$"{RelativeStandardDeviationPercentColumn}," +
			$"{CorrelationCoefficientColumn} " +
			$"FROM {TableName} ";

		internal List<CompoundCalibrationResults> GetCompoundCalibrationResultsByProcessingMethodId(IDbConnection connection, long processingMethodId)
		{
			Log.Debug($"Invoked GetCompoundCalibrationResultsByProcessingMethodId(): processingMethodId={processingMethodId}");

			try
			{
				var results = connection.Query<CompoundCalibrationResults>(
					SelectSql +
					$"WHERE {ProcessingMethodIdColumn} = {processingMethodId}").ToList();

				Log.Debug($"{results.Count} records found in {TableName} table");

				return results;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetCompoundCalibrationResultsByProcessingMethodId()", ex);
				throw;
			}
		}

		internal virtual void Create(IDbConnection connection, List<CompoundCalibrationResults> compoundCalibrationResultsList)
		{
			Log.Debug($"Invoked CreateChildren(): compoundCalibrationResultsList.Count={compoundCalibrationResultsList.Count}");
			try
			{
				if (compoundCalibrationResultsList.Count > 0)
				{
					foreach (var compoundCalibrationResults in compoundCalibrationResultsList)
					{
						compoundCalibrationResults.Id = connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", compoundCalibrationResults);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in Create", ex);
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

		internal CompoundCalibrationResults GetCompoundCalibrationResultsByProcessingMethodIdAndCompoundGuid(IDbConnection connection, long processingMethodId, Guid compoundGuid)
		{
			Log.Debug($"Invoked GetCompoundCalibrationResultsByProcessingMethodIdAndCompoundGuid(): processingMethodId={processingMethodId}");

			try
			{
				return connection.QueryFirstOrDefault<CompoundCalibrationResults>(
					SelectSql +
					$"WHERE {ProcessingMethodIdColumn} = {processingMethodId} AND {GuidColumn}='{compoundGuid}'");


			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetCompoundCalibrationResultsByProcessingMethodIdAndCompoundGuid()", ex);
				throw;
			}
		}

		public bool ClearCurrentCalibration(IDbConnection connection, long compCalibrationResultId)
		{
			try
			{
				var count = connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {TableName}.{IdColumn} = {compCalibrationResultId}");

				return count > 0;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in ClearCurrentCalibration method", ex);
				throw;
			}
		}

		internal List<Guid> GetCompoundGuidsFromCalibrationResultsByProcessingMethodId(IDbConnection connection, long processingMethodId)
		{
			Log.Debug($"Invoked GetCompoundCalibrationResultsByProcessingMethodId(): processingMethodId={processingMethodId}");

			try
			{
				var results = connection.Query<Guid>($"SELECT " +
				                                     $"{GuidColumn} " +
				                                     $"FROM {TableName} " +
				                                     $"WHERE {ProcessingMethodIdColumn} = {processingMethodId}").ToList();

				Log.Debug($"{results.Count} records found in {TableName} table");

				return results;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetCompoundGuidsFromCalibrationResultsByProcessingMethodId()", ex);
				throw;
			}
		}
	}
}
