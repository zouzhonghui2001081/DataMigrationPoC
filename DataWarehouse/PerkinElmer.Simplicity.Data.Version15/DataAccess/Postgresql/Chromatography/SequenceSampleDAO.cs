using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class SequenceSampleDao : SequenceSampleDaoBase
    {
        internal static string TableName { get; } = "SequenceSampleInfo";
        internal static string SequenceIdColumn { get; } = "SequenceId";

        protected readonly string InsertSql = $"INSERT INTO {TableName} ({SequenceIdColumn}," +
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
                                              $"{AcquisitionMethodNameColumn}," +
                                              $"{AcquisitionMethodVersionNumberColumn}," +
                                              $"{ProcessingMethodNameColumn}," +
                                              $"{ProcessingMethodVersionNumberColumn}," +
                                              $"{CalibrationCurveNameColumn}," +
                                              $"{InjectionPortAsIntegerColumn}," +
                                              $"{InjectionPortAsIntegerDeviceNameColumn}," +
                                              $"{InjectionPortColumn}," +
                                              $"{InjectionTypeAsIntegerColumn}," +
                                              $"{InjectionTypeAsIntegerDeviceNameColumn}," +
                                              $"{SampleAmountColumn}," +
                                              $"{DilutionFactorColumn}," +
                                              $"{AddendColumn}," +
                                              $"{StandardAmountAdjustmentColumn}," +
                                              $"{SampleReportTemplateColumn}," +
                                              $"{SummaryReportGroupColumn}," +
                                              $"{NormalizationFactorColumn}," +
                                              $"{SuitabilitySampleTypeColumn}) " +
                                              $"VALUES (@{SequenceIdColumn}," +
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
                                              $"@{AcquisitionMethodNameColumn}," +
                                              $"@{AcquisitionMethodVersionNumberColumn}," +
                                              $"@{ProcessingMethodNameColumn}," +
                                              $"@{ProcessingMethodVersionNumberColumn}," +
                                              $"@{CalibrationCurveNameColumn}," +
                                              $"@{InjectionPortAsIntegerColumn}," +
                                              $"@{InjectionPortAsIntegerDeviceNameColumn}," +
                                              $"@{InjectionPortColumn}," +
                                              $"@{InjectionTypeAsIntegerColumn}," +
                                              $"@{InjectionTypeAsIntegerDeviceNameColumn}," +
                                              $"@{SampleAmountColumn}," +
                                              $"@{DilutionFactorColumn}," +
                                              $"@{AddendColumn}," +
                                              $"@{StandardAmountAdjustmentColumn}," +
                                              $"@{SampleReportTemplateColumn}," +
                                              $"@{SummaryReportGroupColumn}," +
                                              $"@{NormalizationFactorColumn}," +
                                              $"@{SuitabilitySampleTypeColumn}) ";

        protected readonly string SelectSql = $"SELECT {IdColumn}," +
                                              $"{SequenceIdColumn}," +
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
                                              $"{SampleReportTemplateColumn}," +
                                              $"{SummaryReportGroupColumn}," +
                                              $"{NormalizationFactorColumn}," +
                                              $"{SuitabilitySampleTypeColumn} " +
                                              $"FROM {TableName} ";

		public List<SequenceSampleInfo> GetSequenceSampleInfoBySequenceId(IDbConnection connection, long sequenceId)
        {
            List<SequenceSampleInfo> sequenceSamples = connection.Query<SequenceSampleInfo>(
	            SelectSql +
	            $"WHERE {SequenceIdColumn} = '{sequenceId}'").ToList();

            return sequenceSamples;
        }

        public void SaveAsSequenceSamples(IDbConnection connection, SequenceSampleInfo[] sequenceSamplesInfo)
        {
            connection.Execute(InsertSql, sequenceSamplesInfo);
        }

        public void SaveSequenceSamples(IDbConnection connection, long sequenceId, SequenceSampleInfo[] sequenceSamplesInfo)
        {
            DeleteSequenceSamples(connection, sequenceId);
            SaveAsSequenceSamples(connection, sequenceSamplesInfo);
        }

        public void DeleteSequenceSamples(IDbConnection connection, long sequenceId)
        {
            connection.Query(
                $"DELETE FROM {TableName} " +
                $"WHERE {SequenceIdColumn} = {sequenceId}");
        }
    }
}
