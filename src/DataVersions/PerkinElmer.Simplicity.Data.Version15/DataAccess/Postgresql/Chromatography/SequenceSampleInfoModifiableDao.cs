using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class SequenceSampleInfoModifiableDao : SequenceSampleDaoBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static string TableName { get; } = "SequenceSampleInfoModifiable";
        internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";

        protected readonly string InsertSql = $"INSERT INTO {TableName} ({GuidColumn}," +
                                              $"{SampleNameColumn}," +
                                              $"{SelectedColumn}," +
                                              $"{SampleIdColumn}," +
                                              $"{UserCommentsColumn}," +
                                              $"{SampleTypeColumn}," +
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
                                              $"{InjectionPortColumn}," +
                                              $"{InjectionPortAsIntegerColumn}," +
                                              $"{InjectionPortAsIntegerDeviceNameColumn}," +
                                              $"{AcquisitionMethodNameColumn}," +
											  $"{AcquisitionMethodVersionNumberColumn}," +											  
                                              $"{ProcessingMethodNameColumn}," +
											  $"{ProcessingMethodVersionNumberColumn}," +
                                              $"{CalibrationCurveNameColumn}," +
                                              $"{InjectionTypeAsIntegerColumn}," +
                                              $"{InjectionTypeAsIntegerDeviceNameColumn}," +
                                              $"{AnalysisResultSetIdColumn}," +
                                              $"{SampleAmountColumn}," +
                                              $"{DilutionFactorColumn}," +
                                              $"{AddendColumn}," +
                                              $"{StandardAmountAdjustmentColumn}," +
                                              $"{SampleReportTemplateColumn}, " +
                                              $"{SummaryReportGroupColumn}, " +
                                              $"{NormalizationFactorColumn}," +
                                              $"{SuitabilitySampleTypeColumn}) " +
                                              $"VALUES (@{GuidColumn}," +
                                              $"@{SampleNameColumn}," +
                                              $"@{SelectedColumn}," +
                                              $"@{SampleIdColumn}," +
                                              $"@{UserCommentsColumn}," +
                                              $"@{SampleTypeColumn}," +
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
                                              $"@{InjectionPortColumn}," +
                                              $"@{InjectionPortAsIntegerColumn}," +
                                              $"@{InjectionPortAsIntegerDeviceNameColumn}," +
                                              $"@{AcquisitionMethodNameColumn}," + 
											  $"@{AcquisitionMethodVersionNumberColumn}," +
                                              $"@{ProcessingMethodNameColumn}," +
											  $"@{ProcessingMethodVersionNumberColumn}," +
                                              $"@{CalibrationCurveNameColumn}," +
                                              $"@{InjectionTypeAsIntegerColumn}," +
                                              $"@{InjectionTypeAsIntegerDeviceNameColumn}," +
                                              $"@{AnalysisResultSetIdColumn}," +
                                              $"@{SampleAmountColumn}," +
                                              $"@{DilutionFactorColumn}," +
                                              $"@{AddendColumn}," +
                                              $"@{StandardAmountAdjustmentColumn}," +
                                              $"@{SampleReportTemplateColumn}, " +
                                              $"@{SummaryReportGroupColumn}, " +
                                              $"@{NormalizationFactorColumn}," +
                                              $"@{SuitabilitySampleTypeColumn}) ";

        protected readonly string SelectSql = $"SELECT {IdColumn}," +
                                              $"{AnalysisResultSetIdColumn}," +
                                              $"{GuidColumn}," +
                                              $"{SampleNameColumn}," +
                                              $"{SelectedColumn}," +
                                              $"{SampleIdColumn}," +
                                              $"{UserCommentsColumn}," +
                                              $"{SampleTypeColumn}," +
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
                                              $"{InjectionPortColumn}," +
                                              $"{InjectionPortAsIntegerColumn}," +
                                              $"{InjectionPortAsIntegerDeviceNameColumn}," +
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

		public long SaveSequenceSampleInfoModifiable(IDbConnection connection,
           SequenceSampleInfoModifiable sequenceSampleInfoModifiable)
        {
            try
            {
	            long sequenceSampleInfoModifiableId =
		            connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", sequenceSampleInfoModifiable);
                return sequenceSampleInfoModifiableId;

            }
            catch (Exception exception)
            {
                Log.Error(
                    $"Error occured in SaveChannelMethodModifiable() method of class{GetType().Name} - {exception.Message}");
                throw;
            }
        }

        public SequenceSampleInfoModifiable GetSequenceSampleInfoModifiable(IDbConnection connection,
            long sequenceSampleInfoModifiableId)
        {
            try
            {
                SequenceSampleInfoModifiable sequenceSampleInfoModifiable =
	                connection.QueryFirstOrDefault<SequenceSampleInfoModifiable>(
		                SelectSql + $"WHERE {IdColumn} = {sequenceSampleInfoModifiableId}");
                return sequenceSampleInfoModifiable;
            }
            catch (Exception ex)
            {
                Log.Error(
                    $"Error occured in GetSequenceSampleInfoModifiable() method of class{GetType().Name} - {ex.Message}");
                throw;
            }
        }
        public IList<SequenceSampleInfoModifiable> GetSequenceSampleInfoModifiableByAnalysisResultSetId(IDbConnection connection,
	        long analysisResultSetId)
        {
	        try
	        {
		        return connection.Query<SequenceSampleInfoModifiable>(
			        SelectSql + $"WHERE {AnalysisResultSetIdColumn} = @{AnalysisResultSetIdColumn}",
			        new {AnalysisResultSetId = analysisResultSetId}).ToList();
	        }
	        catch (Exception ex)
	        {
		        Log.Error(
			        $"Error occured in GetSequenceSampleInfoModifiableByAnalysisResultSetId() method of class{GetType().Name} - {ex.Message}");
		        throw;
	        }
        }

		public bool RemoveSequenceSampleInfoModifiable(IDbConnection connection, long sequenceSampleInfoModifiableId)
        {
            try
            {
                connection.Execute(
                    $"Delete from {TableName} where {IdColumn} = {sequenceSampleInfoModifiableId} ");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error in RemoveSequenceSampleInfoModifiable method", ex);
                throw;
            }
        }
        public bool RemoveSequenceSampleInfoModifiableBeforeSavingAnalysisResultSet(IDbConnection connection, long analysisResultSetId)
        {
            try
            {
                connection.Execute(
                    $"Delete from {TableName} where {AnalysisResultSetIdColumn} = {analysisResultSetId} ");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error in RemoveSequenceSampleInfoModifiableBeforeSavingAnalysisResultSet method", ex);
                throw;
            }
        }




    }
}
