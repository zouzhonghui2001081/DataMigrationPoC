using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class SequenceSampleInfoBatchResultDao : SequenceSampleDaoBase
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "SequenceSampleInfoBatchResult";

		internal static string BatchResultSetIdColumn { get; } = "BatchResultSetId";

		internal readonly string SelectSql = "SELECT " +
		                                     $"{IdColumn}," +
		                                     $"{BatchResultSetIdColumn}," +
		                                     $"{GuidColumn}," +
		                                     $"{SampleNameColumn}," +
		                                     $"{SelectedColumn}," +
		                                     $"{SampleIdColumn}," +
		                                     $"{UserCommentsColumn}," +
		                                     $"{SampleTypeColumn}," +
		                                     $"{NumberOfRepeatsColumn}," +
		                                     $"{LevelColumn}," +
		                                     $"{MultiplierColumn}," +
		                                     $"{DivisorColumn}," +
		                                     $"{UnknownAmountAdjustmentColumn}," +
		                                     $"{InternalStandardAmountAdjustmentColumn}," +
		                                     $"{BaselineCorrectionColumn}," +
		                                     $"{BaselineRunIdColumn}," +
		                                     $"{BaselineRunGuidColumn}," +
		                                     $"{RackCodeColumn}," +
		                                     $"{RackPositionColumn}," +
		                                     $"{PlateCodeColumn}," +
		                                     $"{PlateCodeAsIntegerColumn}," +
		                                     $"{PlateCodeAsIntegerDeviceNameColumn}," +
		                                     $"{PlatePositionColumn}," +
		                                     $"{PlatePositionAsIntegerColumn}," +
		                                     $"{PlatePositionAsIntegerDeviceNameColumn}," +
		                                     $"{VialPositionColumn}," +
		                                     $"{VialPositionAsIntegerColumn}," +
		                                     $"{VialPositionAsIntegerDeviceNameColumn}," +
		                                     $"{DestinationVialColumn}," +
		                                     $"{DestinationVialAsIntegerColumn}," +
		                                     $"{DestinationVialAsIntegerDeviceNameColumn}," +
		                                     $"{InjectionVolumeColumn}," +
		                                     $"{InjectionVolumeDeviceNameColumn}," +
		                                     $"{InjectionTypeColumn}," +
		                                     $"{InjectionPortAsIntegerColumn}," +
		                                     $"{InjectionPortAsIntegerDeviceNameColumn}," +
		                                     $"{InjectionPortColumn}," +
		                                     $"{AcquisitionMethodNameColumn}," +
											 $"{AcquisitionMethodVersionNumberColumn}," +
		                                     $"{ProcessingMethodNameColumn}," +
											 $"{ProcessingMethodVersionNumberColumn}," +
											 $"{CalibrationCurveNameColumn}," +
		                                     $"{InjectionTypeAsIntegerColumn}," +
		                                     $"{InjectionTypeAsIntegerDeviceNameColumn}," +
		                                     $"{SampleAmountColumn}," +
		                                     $"{DilutionFactorColumn}," +
		                                     $"{AddendColumn}," +
		                                     $"{StandardAmountAdjustmentColumn}," +
		                                     $"{SampleReportTemplateColumn}, " +
		                                     $"{SummaryReportGroupColumn}, " +
		                                     $"{NormalizationFactorColumn}," +
		                                     $"{SuitabilitySampleTypeColumn} " +
		                                     $"FROM {TableName} ";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({BatchResultSetIdColumn}," +
		                                      $"{GuidColumn}," +
		                                      $"{SampleNameColumn}," +
		                                      $"{SelectedColumn}," +
		                                      $"{SampleIdColumn}," +
		                                      $"{UserCommentsColumn}," +
		                                      $"{SampleTypeColumn}," +
		                                      $"{NumberOfRepeatsColumn}," +
		                                      $"{LevelColumn}," +
		                                      $"{MultiplierColumn}," +
		                                      $"{DivisorColumn}," +
		                                      $"{UnknownAmountAdjustmentColumn}," +
		                                      $"{InternalStandardAmountAdjustmentColumn}," +
		                                      $"{BaselineCorrectionColumn}," +
		                                      $"{BaselineRunIdColumn}," +
		                                      $"{BaselineRunGuidColumn}," +
		                                      $"{RackCodeColumn}," +
		                                      $"{RackPositionColumn}," +
		                                      $"{PlateCodeColumn}," +
		                                      $"{PlateCodeAsIntegerColumn}," +
		                                      $"{PlateCodeAsIntegerDeviceNameColumn}," +
		                                      $"{PlatePositionColumn}," +
		                                      $"{PlatePositionAsIntegerColumn}," +
		                                      $"{PlatePositionAsIntegerDeviceNameColumn}," +
		                                      $"{VialPositionColumn}," +
		                                      $"{VialPositionAsIntegerColumn}," +
		                                      $"{VialPositionAsIntegerDeviceNameColumn}," +
		                                      $"{DestinationVialColumn}," +
		                                      $"{DestinationVialAsIntegerColumn}," +
		                                      $"{DestinationVialAsIntegerDeviceNameColumn}," +
		                                      $"{InjectionVolumeColumn}," +
		                                      $"{InjectionVolumeDeviceNameColumn}," +
		                                      $"{InjectionTypeColumn}," +
		                                      $"{InjectionPortAsIntegerColumn}," +
		                                      $"{InjectionPortAsIntegerDeviceNameColumn}," +
		                                      $"{InjectionPortColumn}," +
		                                      $"{AcquisitionMethodNameColumn}," +
											  $"{AcquisitionMethodVersionNumberColumn}," +
		                                      $"{ProcessingMethodNameColumn}," +
											  $"{ProcessingMethodVersionNumberColumn}," +
											  $"{CalibrationCurveNameColumn}," +
		                                      $"{InjectionTypeAsIntegerColumn}," +
		                                      $"{InjectionTypeAsIntegerDeviceNameColumn}," +
		                                      $"{SampleAmountColumn}," +
		                                      $"{DilutionFactorColumn}," +
		                                      $"{AddendColumn}," +
		                                      $"{StandardAmountAdjustmentColumn}," +
		                                      $"{SampleReportTemplateColumn}, " +
		                                      $"{SummaryReportGroupColumn}, " +
		                                      $"{NormalizationFactorColumn}," +
		                                      $"{SuitabilitySampleTypeColumn}) " +
		                                      $"VALUES (@{BatchResultSetIdColumn}," +
		                                      $"@{GuidColumn}," +
		                                      $"@{SampleNameColumn}," +
		                                      $"@{SelectedColumn}," +
		                                      $"@{SampleIdColumn}," +
		                                      $"@{UserCommentsColumn}," +
		                                      $"@{SampleTypeColumn}," +
		                                      $"@{NumberOfRepeatsColumn}," +
		                                      $"@{LevelColumn}," +
		                                      $"@{MultiplierColumn}," +
		                                      $"@{DivisorColumn}," +
		                                      $"@{UnknownAmountAdjustmentColumn}," +
		                                      $"@{InternalStandardAmountAdjustmentColumn}," +
		                                      $"@{BaselineCorrectionColumn}," +
		                                      $"@{BaselineRunIdColumn}," +
		                                      $"@{BaselineRunGuidColumn}," +
		                                      $"@{RackCodeColumn}," +
		                                      $"@{RackPositionColumn}," +
		                                      $"@{PlateCodeColumn}," +
		                                      $"@{PlateCodeAsIntegerColumn}," +
		                                      $"@{PlateCodeAsIntegerDeviceNameColumn}," +
		                                      $"@{PlatePositionColumn}," +
		                                      $"@{PlatePositionAsIntegerColumn}," +
		                                      $"@{PlatePositionAsIntegerDeviceNameColumn}," +
		                                      $"@{VialPositionColumn}," +
		                                      $"@{VialPositionAsIntegerColumn}," +
		                                      $"@{VialPositionAsIntegerDeviceNameColumn}," +
		                                      $"@{DestinationVialColumn}," +
		                                      $"@{DestinationVialAsIntegerColumn}," +
		                                      $"@{DestinationVialAsIntegerDeviceNameColumn}," +
		                                      $"@{InjectionVolumeColumn}," +
		                                      $"@{InjectionVolumeDeviceNameColumn}," +
		                                      $"@{InjectionTypeColumn}," +
		                                      $"@{InjectionPortAsIntegerColumn}," +
		                                      $"@{InjectionPortAsIntegerDeviceNameColumn}," +
		                                      $"@{InjectionPortColumn}," +
		                                      $"@{AcquisitionMethodNameColumn}," +
											  $"@{AcquisitionMethodVersionNumberColumn}," +
		                                      $"@{ProcessingMethodNameColumn}," +
											  $"@{ProcessingMethodVersionNumberColumn}," +
											  $"@{CalibrationCurveNameColumn}," +
		                                      $"@{InjectionTypeAsIntegerColumn}," +
		                                      $"@{InjectionTypeAsIntegerDeviceNameColumn}," +
		                                      $"@{SampleAmountColumn}," +
		                                      $"@{DilutionFactorColumn}," +
		                                      $"@{AddendColumn}," +
		                                      $"@{StandardAmountAdjustmentColumn}," +
		                                      $"@{SampleReportTemplateColumn}, " +
		                                      $"@{SummaryReportGroupColumn}, " +
		                                      $"@{NormalizationFactorColumn}," +
		                                      $"@{SuitabilitySampleTypeColumn}) ";

		public SequenceSampleInfoBatchResult GetSequenceSampleInfoBatchResultById(IDbConnection connection,
			long sequenceSampleId)
		{
			try
			{

			    return connection.QueryFirstOrDefault<SequenceSampleInfoBatchResult>(SelectSql +
			                                                                         $" WHERE {IdColumn} = {sequenceSampleId}");
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetSequenceSampleInfoBatchResultById method", ex);
				throw;
			}
		}

		public bool CreateSequenceSampleInfo(IDbConnection connection, SequenceSampleInfoBatchResult sequenceSamplesInfo)
		{
			try
			{
				sequenceSamplesInfo.Id = connection.ExecuteScalar<long>(InsertSql + " RETURNING Id", sequenceSamplesInfo);
				return sequenceSamplesInfo.Id != 0;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in CreateSequenceSampleInfo method", ex);
				throw;
			}
		}

		public SequenceSampleInfoBatchResult GetSequenceSampleByGuid(IDbConnection connection, Guid sequenceSampleGuid)
		{
			try
			{
				return connection.QueryFirstOrDefault<SequenceSampleInfoBatchResult>(
                                                        SelectSql +
                                                        $"WHERE {GuidColumn} = '{sequenceSampleGuid}'");
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetSequenceSampleByGuid method", ex);
				throw;
			}
		}

		public SequenceSampleInfoBatchResult[] GetSequenceSampleInfoBatchResult(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				try
				{
					return connection.Query<SequenceSampleInfoBatchResult>(
						$"{SelectSql} " +
						$"WHERE {BatchResultSetIdColumn} = {batchResultSetId}").ToArray();
				}
				catch (Exception ex)
				{
					Log.Error("Error in GetSequenceSampleInfoBatchResult method", ex);
					throw;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in SelectAll method", ex);
				throw;
			}
		}
		public bool DeleteByBatchResultSetId(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} WHERE {BatchResultSetIdColumn} = @{BatchResultSetIdColumn}",
					new { BatchResultSetId = batchResultSetId});
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteByBatchResultSetId method", ex);
				throw;
			}
		}
	}
}
