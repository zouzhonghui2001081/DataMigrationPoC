using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class CalibrationPointResponseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "CalibrationPointResponse";
		public static string IdColumn { get; } = "Id";
        public static string CompoundCalibrationResultsIdColumn { get; } = "CompoundCalibrationResultsId";
		public static string LevelColumn { get; } = "Level";
		public static string QuantifyUsingAreaColumn { get; } = "QuantifyUsingArea";
		public static string UseInternalStandardColumn { get; } = "UseInternalStandard";
		public static string AreaColumn { get; } = "Area";
		public static string AreaRatioColumn { get; } = "AreaRatio";
		public static string HeightColumn { get; } = "Height";
        public static string PeakNotFoundErrorColumn { get; } = "PeakNotFoundError";
        public static string InternalStandardPeakNotFoundErrorColumn { get; } = "InternalStandardPeakNotFoundError";
        public static string HeightRatioColumn { get; } = "HeightRatio";
		public static string ExcludedColumn { get; } = "Excluded";
		public static string BatchRunGuidColumn { get; } = "BatchRunGuid";
		public static string ExternalColumn { get; } = "External";
		public static string PeakAreaPercentageColumn { get; } = "PeakAreaPercentage";
		public static string PointCalibrationFactorColumn { get; } = "PointCalibrationFactor";
		public static string InvalidAmountErrorColumn { get; } = "InvalidAmountError";
		public static string OutlierTestFailedColumn { get; } = "OutlierTestFailed";
		public static string OutlierTestResultColumn { get; } = "OutlierTestResult";
		public static string StandardAmountAdjustmentCoeffColumn { get; } = "StandardAmountAdjustmentCoeff";
		public static string InternalStandardAmountAdjustmentCoeffColumn { get; } = "InternalStandardAmountAdjustmentCoeff";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({CompoundCalibrationResultsIdColumn}," +
		                                      $"{LevelColumn}," +
		                                      $"{QuantifyUsingAreaColumn}," +
		                                      $"{UseInternalStandardColumn}," +
		                                      $"{AreaColumn}," +
		                                      $"{AreaRatioColumn}," +
		                                      $"{HeightColumn}," +
		                                      $"{HeightRatioColumn}," +
		                                      $"{ExcludedColumn}," +
		                                      $"{BatchRunGuidColumn}," +
		                                      $"{ExternalColumn}," +
		                                      $"{PeakAreaPercentageColumn}," +
		                                      $"{PointCalibrationFactorColumn}," +
		                                      $"{InvalidAmountErrorColumn}," +
		                                      $"{OutlierTestFailedColumn}," +
		                                      $"{OutlierTestResultColumn}," +
		                                      $"{StandardAmountAdjustmentCoeffColumn}," +
		                                      $"{InternalStandardAmountAdjustmentCoeffColumn}," +
		                                      $"{PeakNotFoundErrorColumn}," +
		                                      $"{InternalStandardPeakNotFoundErrorColumn}" +
		                                      $") VALUES" +
		                                      $"(@{CompoundCalibrationResultsIdColumn}," +
		                                      $"@{LevelColumn}," +
		                                      $"@{QuantifyUsingAreaColumn}," +
		                                      $"@{UseInternalStandardColumn}," +
		                                      $"@{AreaColumn}," +
		                                      $"@{AreaRatioColumn}," +
		                                      $"@{HeightColumn}," +
		                                      $"@{HeightRatioColumn}," +
		                                      $"@{ExcludedColumn}," +
		                                      $"@{BatchRunGuidColumn}," +
		                                      $"@{ExternalColumn}," +
		                                      $"@{PeakAreaPercentageColumn}," +
		                                      $"@{PointCalibrationFactorColumn}," +
		                                      $"@{InvalidAmountErrorColumn}," +
		                                      $"@{OutlierTestFailedColumn}," +
		                                      $"@{OutlierTestResultColumn}," +
		                                      $"@{StandardAmountAdjustmentCoeffColumn}," +
		                                      $"@{InternalStandardAmountAdjustmentCoeffColumn}," +
		                                      $"@{PeakNotFoundErrorColumn}," +
		                                      $"@{InternalStandardPeakNotFoundErrorColumn}) ";

		protected readonly string SelectSql =
			"SELECT " +
			$"{IdColumn}," +
			$"{CompoundCalibrationResultsIdColumn}," +
			$"{LevelColumn}," +
			$"{QuantifyUsingAreaColumn}," +
			$"{UseInternalStandardColumn}," +
			$"{AreaColumn}," +
			$"{AreaRatioColumn}," +
			$"{HeightColumn}," +
			$"{HeightRatioColumn}," +
			$"{ExcludedColumn}," +
			$"{BatchRunGuidColumn}," +
			$"{ExternalColumn}," +
			$"{PeakAreaPercentageColumn}," +
			$"{PointCalibrationFactorColumn}," +
			$"{InvalidAmountErrorColumn}," +
			$"{OutlierTestFailedColumn}," +
			$"{OutlierTestResultColumn}," +
			$"{StandardAmountAdjustmentCoeffColumn}," +
			$"{InternalStandardAmountAdjustmentCoeffColumn}," +
			$"{PeakNotFoundErrorColumn}, " +
			$"{InternalStandardPeakNotFoundErrorColumn} " +
			$"FROM {TableName} ";

		public List<CalibrationPointResponse> GetCalibrationPointResponseByCompoundCalibrationResultId(IDbConnection connection, long compoundCalibrationResultId)
		{
			Log.Debug($"Invoked CompoundCalibrationResultsModifiable(): compoundCalibrationResultId={compoundCalibrationResultId}");
			try
			{
				var result = connection.Query<CalibrationPointResponse>(
					SelectSql +
					$"WHERE {CompoundCalibrationResultsIdColumn} = {compoundCalibrationResultId}").ToList();

				Log.Debug($"{result.Count} records found in {TableName} table");
				return result;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in GetCalibrationPointResponseByCompoundCalibrationResultId()", ex);
				throw;
			}
		}

		public virtual void Create(IDbConnection connection, IList<CalibrationPointResponse> calibrationPointResponseBatchResultList)
		{
			try
			{
				var insertCount = connection.Execute(InsertSql, calibrationPointResponseBatchResultList);
				Log.Debug($"{insertCount} records inserted in {TableName} table");
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in CreateChildren()", ex);
				throw;
			}
		}

		public List<long> GetExternalCalibrationPointResponseByCompoundCalibrationResultId(IDbConnection connection, long compoundCalibrationResultId)
		{
			Log.Debug($"Invoked CompoundCalibrationResultsModifiable(): compoundCalibrationResultId={compoundCalibrationResultId}");
			try
			{

				string sql = "SELECT " +
							 $"{CompoundCalibrationResultsIdColumn} " +
							 $"FROM {TableName} " +
							 $"WHERE {CompoundCalibrationResultsIdColumn} = {compoundCalibrationResultId} AND {ExternalColumn} = '{true}'";

				var result = connection.Query<long>(sql).ToList();
				Log.Debug($"{result.Count} records found in {TableName} table");
				return result;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in GetExternalCalibrationPointResponseByCompoundCalibrationResultId()", ex);
				throw;
			}
		}

		public bool Delete(IDbConnection connection, List<long> compoundCalibrationResultIds)
		{
			try
			{
				foreach (var compoundCalibrationResultId in compoundCalibrationResultIds)
				{
					connection.Execute(
						$"DELETE FROM {TableName} " +
						$"WHERE {TableName}.{CompoundCalibrationResultsIdColumn} = {compoundCalibrationResultId}");
				}

				return true;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
	}
}
