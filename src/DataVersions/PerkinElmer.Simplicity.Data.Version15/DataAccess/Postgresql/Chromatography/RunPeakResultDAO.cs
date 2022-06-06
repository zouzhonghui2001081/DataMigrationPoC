using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;
using NpgsqlTypes;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class RunPeakResultDao
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static string TableName { get; } = "RunPeakResult";
        internal static string IdColumn { get; } = "Id";
        internal static string CalculatedChannelDataIdColumn { get; } = "CalculatedChannelDataId";
        internal static string BatchRunChannelGuidColumn { get; } = "BatchRunChannelGuid";
        internal static string CompoundGuidColumn { get; } = "CompoundGuid";
        internal static string ChannelGuidColumn { get; } = "ChannelGuid";
        internal static string PeakNumberColumn { get; } = "PeakNumber";
        internal static string AreaColumn { get; } = "Area";
        internal static string HeightColumn { get; } = "Height";
        internal static string InternalStandardAreaRatioColumn { get; } = "InternalStandardAreaRatio";
        internal static string InternalStandardHeightRatioColumn { get; } = "InternalStandardHeightRatio";
        internal static string AreaPercentColumn { get; } = "AreaPercent";
        internal static string RetentionTimeColumn { get; } = "RetentionTime";
        internal static string StartPeakTimeColumn { get; } = "StartPeakTime";
        internal static string EndPeakTimeColumn { get; } = "EndPeakTime";
        internal static string BaselineSlopeColumn { get; } = "BaselineSlope";
        internal static string BaselineInterceptColumn { get; } = "BaselineIntercept";
        internal static string SignalToNoiseRatioColumn { get; } = "SignalToNoiseRatio";
        internal static string AmountColumn { get; } = "Amount";
        internal static string InternalStandardAmountRatioColumn { get; } = "InternalStandardAmountRatio";
        internal static string AreaToHeightRatioColumn { get; } = "AreaToHeightRatio";
        internal static string AreaToAmountRatioColumn { get; } = "AreaToAmountRatio";
        internal static string BaselineCodeColumn { get; } = "BaselineCode";
        internal static string CalibrationInRangeColumn { get; } = "CalibrationInRange";
        internal static string KPrimeColumn { get; } = "KPrime";
        internal static string NormalizedAmountColumn { get; } = "NormalizedAmount";
        internal static string RelativeRetentionTimeColumn { get; } = "RelativeRetentionTime";
        internal static string RawAmountColumn { get; } = "RawAmount";
        internal static string TailingFactorColumn { get; } = "TailingFactor";
        internal static string ResolutionColumn { get; } = "Resolution";
        internal static string PeakWidthColumn { get; } = "PeakWidth";
        internal static string PeakWidthAtHalfHeightColumn { get; } = "PeakWidthAtHalfHeight";
        internal static string PeakGroupGuidColumn { get; } = "PeakGroupGuid";
        internal static string PeakNameColumn { get; } = "PeakName";
        internal static string PeakGroupColumn { get; } = "PeakGroup";
        internal static string OverlappedColumn { get; } = "Overlapped";
        internal static string IsBaselineExpoColumn { get; } = "IsBaselineExpo";
        internal static string ExpoAColumn { get; } = "ExpoA";
        internal static string ExpoBColumn { get; } = "ExpoB";
        internal static string ExpoCorrectionColumn { get; } = "ExpoCorrection";
        internal static string ExpoDecayColumn { get; } = "ExpoDecay";
        internal static string RetTimeReferenceGuidColumn { get; } = "RetTimeReferenceGuid";
        internal static string RrtReferenceGuidColumn { get; } = "RrtReferenceGuid";
        internal static string InternalStandardGuidColumn { get; } = "InternalStandardGuid";
        internal static string PlatesDorseyFoleyColumn { get; } = "PlatesDorseyFoley";
        internal static string PlatesTangentialColumn { get; } = "PlatesTangential";
        internal static string PeakWidthAt5PercentHeightColumn { get; } = "PeakWidthAt5PercentHeight";
        internal static string PeakWidthAt10PercentHeightColumn { get; } = "PeakWidthAt10PercentHeight";
        internal static string RelativeRetTimeSuitColumn { get; } = "RelativeRetTimeSuit";
        internal static string SignalColumn { get; } = "Signal";
        internal static string ExpoHeightColumn { get; } = "ExpoHeight";
        internal static string PeakGuidColumn { get; } = "PeakGuid";
        internal static string InternalStandardAmountColumn { get; } = "InternalStandardAmount";
        internal static string ReferenceInternalStandardCompoundGuidColumn { get; } = "ReferenceInternalStandardCompoundGuid";
        internal static string AmountErrorColumn { get; } = "AmountError";
        internal static string CompoundTypeColumn { get; } = "CompoundType";
        internal static string AbsorbanceRatioColumn { get; } = "AbsorbanceRatio";
        internal static string StandardConfirmationIndexColumn { get; } = "StandardConfirmationIndex";
        internal static string StandardConfirmationPassedColumn { get; } = "StandardConfirmationPassed";
        internal static string StandardConfirmationErrorColumn { get; } = "StandardConfirmationError";
        internal static string WavelengthMaxColumn { get; } = "WavelengthMax";
        internal static string AbsorbanceAtWavelengthMaxColumn { get; } = "AbsorbanceAtWavelengthMax";
        internal static string WavelengthMaxErrorColumn { get; } = "WavelengthMaxError";
        internal static string PeakPurityColumn { get; } = "PeakPurity";
        internal static string PeakPurityPassedColumn { get; } = "PeakPurityPassed";
        internal static string PeakPurityErrorColumn { get; } = "PeakPurityError";
        internal static string DataSourceTypeColumn { get; } = "DataSourceType";
        internal static string AbsorbanceRatioErrorColumn { get; } = "AbsorbanceRatioError";
        internal static string ManuallyOverridenColumn { get; } = "ManuallyOverriden";
        internal static string MidIndexColumn { get; } = "MidIndex";
        internal static string StartIndexColumn { get; } = "StartIndex";
        internal static string StopIndexColumn { get; } = "StopIndex";
        internal static string LibraryCompoundColumn { get; } = "LibraryCompound";
        internal static string LibraryNameColumn { get; } = "LibraryName";
        internal static string SearchLibraryColumn { get; } = "SearchLibrary";
        internal static string LibraryGuidColumn { get; } = "LibraryGuid";
        internal static string LibraryConfirmationColumn { get; } = "LibraryConfirmation";
        internal static string HitQualityValueColumn { get; } = "HitQualityValue";
        internal static string SearchMatchColumn { get; } = "SearchMatch";
        internal static string CompoundAssignmentTypeColumn { get; } = "CompoundAssignmentType";
        internal static string SearchLibraryCompoundColumn { get; } = "SearchLibraryCompound";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} (" +
						$"{CalculatedChannelDataIdColumn}," +
						$"{BatchRunChannelGuidColumn}," +
						$"{AmountColumn}," +
						$"{AreaColumn}," +
						$"{AreaPercentColumn}," +
						$"{AreaToAmountRatioColumn}," +
						$"{AreaToHeightRatioColumn}," +
						$"{BaselineCodeColumn}," +
						$"{BaselineInterceptColumn}," +
						$"{BaselineSlopeColumn}," +
						$"{CalibrationInRangeColumn}," +
						$"{ChannelGuidColumn}," +
						$"{CompoundGuidColumn}," +
						$"{EndPeakTimeColumn}," +
						$"{ExpoAColumn}," +
						$"{ExpoBColumn}, " +
						$"{ExpoCorrectionColumn}," +
						$"{ExpoDecayColumn}," +
						$"{ExpoHeightColumn}," +
						$"{HeightColumn}," +
						$"{InternalStandardAmountRatioColumn}," +
						$"{InternalStandardAreaRatioColumn}, " +
						$"{InternalStandardGuidColumn}," +
						$"{InternalStandardHeightRatioColumn}," +
						$"{IsBaselineExpoColumn}," +
						$"{KPrimeColumn}," +
						$"{NormalizedAmountColumn}," +
						$"{OverlappedColumn}," +
						$"{PeakGroupColumn}," +
						$"{PeakGroupGuidColumn}," +
						$"{PeakGuidColumn}," +
						$"{PeakNameColumn}," +
						$"{PeakNumberColumn}," +
						$"{PeakWidthAt10PercentHeightColumn}," +
						$"{PeakWidthAt5PercentHeightColumn}," +
						$"{PeakWidthAtHalfHeightColumn}," +
						$"{PeakWidthColumn}," +
						$"{PlatesDorseyFoleyColumn}," +
						$"{PlatesTangentialColumn}," +
						$"{RawAmountColumn}," +
						$"{RelativeRetentionTimeColumn}," +
						$"{RelativeRetTimeSuitColumn}," +
						$"{ResolutionColumn}," +
						$"{RetentionTimeColumn}," +
						$"{RetTimeReferenceGuidColumn}," +
						$"{RrtReferenceGuidColumn}," +
						$"{SignalColumn}," +
						$"{SignalToNoiseRatioColumn}," +
						$"{StartPeakTimeColumn}," +
						$"{TailingFactorColumn}," +
						$"{InternalStandardAmountColumn}," +
						$"{ReferenceInternalStandardCompoundGuidColumn}," +
						$"{AmountErrorColumn}," +
						$"{CompoundTypeColumn}," +
						$"{AbsorbanceRatioColumn}," +
						$"{StandardConfirmationIndexColumn}," +
						$"{StandardConfirmationPassedColumn}," +
						$"{StandardConfirmationErrorColumn}," +
						$"{WavelengthMaxColumn}," +
						$"{AbsorbanceAtWavelengthMaxColumn}," +
						$"{WavelengthMaxErrorColumn}," +
						$"{PeakPurityColumn}," +
						$"{PeakPurityPassedColumn}," +
						$"{PeakPurityErrorColumn}," +
						$"{DataSourceTypeColumn}," +
						$"{AbsorbanceRatioErrorColumn}," +
						$"{ManuallyOverridenColumn}," +
						$"{MidIndexColumn}," +
						$"{StartIndexColumn}," +
						$"{StopIndexColumn}," +
						$"{LibraryNameColumn}," +
						$"{SearchLibraryColumn}," +
						$"{LibraryGuidColumn}," +
						$"{LibraryCompoundColumn}," +
						$"{SearchLibraryCompoundColumn}," +
						$"{LibraryConfirmationColumn}," +
						$"{HitQualityValueColumn}," +
						$"{SearchMatchColumn}," +
						$"{CompoundAssignmentTypeColumn}" +
						") " +
						"VALUES (" +
						$"@{CalculatedChannelDataIdColumn}," +
						$"@{BatchRunChannelGuidColumn}," +
						$"@{AmountColumn}," +
						$"@{AreaColumn}," +
						$"@{AreaPercentColumn}," +
						$"@{AreaToAmountRatioColumn}," +
						$"@{AreaToHeightRatioColumn}," +
						$"@{BaselineCodeColumn}," +
						$"@{BaselineInterceptColumn}," +
						$"@{BaselineSlopeColumn}," +
						$"@{CalibrationInRangeColumn}," +
						$"@{ChannelGuidColumn}," +
						$"@{CompoundGuidColumn}," +
						$"@{EndPeakTimeColumn}," +
						$"@{ExpoAColumn}," +
						$"@{ExpoBColumn}, " +
						$"@{ExpoCorrectionColumn}," +
						$"@{ExpoDecayColumn}," +
						$"@{ExpoHeightColumn}," +
						$"@{HeightColumn}," +
						$"@{InternalStandardAmountRatioColumn}," +
						$"@{InternalStandardAreaRatioColumn}, " +
						$"@{InternalStandardGuidColumn}," +
						$"@{InternalStandardHeightRatioColumn}," +
						$"@{IsBaselineExpoColumn}," +
						$"@{KPrimeColumn}," +
						$"@{NormalizedAmountColumn}," +
						$"@{OverlappedColumn}," +
						$"@{PeakGroupColumn}," +
						$"@{PeakGroupGuidColumn}," +
						$"@{PeakGuidColumn}," +
						$"@{PeakNameColumn}," +
						$"@{PeakNumberColumn}," +
						$"@{PeakWidthAt10PercentHeightColumn}," +
						$"@{PeakWidthAt5PercentHeightColumn}," +
						$"@{PeakWidthAtHalfHeightColumn}," +
						$"@{PeakWidthColumn}," +
						$"@{PlatesDorseyFoleyColumn}," +
						$"@{PlatesTangentialColumn}," +
						$"@{RawAmountColumn}," +
						$"@{RelativeRetentionTimeColumn}," +
						$"@{RelativeRetTimeSuitColumn}," +
						$"@{ResolutionColumn}," +
						$"@{RetentionTimeColumn}," +
						$"@{RetTimeReferenceGuidColumn}," +
						$"@{RrtReferenceGuidColumn}," +
						$"@{SignalColumn}," +
						$"@{SignalToNoiseRatioColumn}," +
						$"@{StartPeakTimeColumn}," +
						$"@{TailingFactorColumn}," +
						$"@{InternalStandardAmountColumn}," +
						$"@{ReferenceInternalStandardCompoundGuidColumn}," +
						$"@{AmountErrorColumn}," +
						$"@{CompoundTypeColumn}," +
						$"@{AbsorbanceRatioColumn}," +
						$"@{StandardConfirmationIndexColumn}," +
						$"@{StandardConfirmationPassedColumn}," +
						$"@{StandardConfirmationErrorColumn}," +
						$"@{WavelengthMaxColumn}," +
						$"@{AbsorbanceAtWavelengthMaxColumn}," +
						$"@{WavelengthMaxErrorColumn}," +
						$"@{PeakPurityColumn}," +
						$"@{PeakPurityPassedColumn}," +
						$"@{PeakPurityErrorColumn}," +
						$"@{DataSourceTypeColumn}," +
						$"@{AbsorbanceRatioErrorColumn}," +
						$"@{ManuallyOverridenColumn}," +
						$"@{MidIndexColumn}," +
						$"@{StartIndexColumn}," +
						$"@{StopIndexColumn}," +
						$"@{LibraryNameColumn}," +
						$"@{SearchLibraryColumn}," +
						$"@{LibraryGuidColumn}," +
						$"@{LibraryCompoundColumn}," +
						$"@{SearchLibraryCompoundColumn}," +
						$"@{LibraryConfirmationColumn}," +
						$"@{HitQualityValueColumn}," +
						$"@{SearchMatchColumn}," +
						$"@{CompoundAssignmentTypeColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {IdColumn}," +
			$"{CalculatedChannelDataIdColumn}," +
			$"{BatchRunChannelGuidColumn}," +
			$"{CompoundGuidColumn}," +
			$"{ChannelGuidColumn}," +
			$"{PeakNumberColumn}," +
			$"{AreaColumn}," +
			$"{HeightColumn}," +
			$"{InternalStandardAreaRatioColumn}," +
			$"{InternalStandardHeightRatioColumn}," +
			$"{AreaPercentColumn}," +
			$"{RetentionTimeColumn}," +
			$"{StartPeakTimeColumn}," +
			$"{EndPeakTimeColumn}," +
			$"{BaselineSlopeColumn}," +
			$"{BaselineInterceptColumn}," +
			$"{SignalToNoiseRatioColumn}," +
			$"{AmountColumn}," +
			$"{InternalStandardAmountRatioColumn}," +
			$"{AreaToHeightRatioColumn}," +
			$"{AreaToAmountRatioColumn}," +
			$"{BaselineCodeColumn}," +
			$"{CalibrationInRangeColumn}," +
			$"{KPrimeColumn}," +
			$"{NormalizedAmountColumn}," +
			$"{RelativeRetentionTimeColumn}," +
			$"{RawAmountColumn}," +
			$"{TailingFactorColumn}," +
			$"{ResolutionColumn}," +
			$"{PeakWidthColumn}," +
			$"{PeakWidthAtHalfHeightColumn}," +
			$"{PeakGroupGuidColumn}," +
			$"{PeakNameColumn}," +
			$"{PeakGroupColumn}," +
			$"{OverlappedColumn}," +
			$"{IsBaselineExpoColumn}," +
			$"{ExpoAColumn}," +
			$"{ExpoBColumn}," +
			$"{ExpoCorrectionColumn}," +
			$"{ExpoDecayColumn}," +
			$"{RetTimeReferenceGuidColumn}," +
			$"{RrtReferenceGuidColumn}," +
			$"{InternalStandardGuidColumn}," +
			$"{PlatesDorseyFoleyColumn}," +
			$"{PlatesTangentialColumn}," +
			$"{PeakWidthAt5PercentHeightColumn}," +
			$"{PeakWidthAt10PercentHeightColumn}," +
			$"{RelativeRetTimeSuitColumn}," +
			$"{SignalColumn}," +
			$"{ExpoHeightColumn}," +
			$"{PeakGuidColumn}," +
			$"{InternalStandardAmountColumn}," +
			$"{ReferenceInternalStandardCompoundGuidColumn}," +
			$"{AmountErrorColumn}," +
			$"{CompoundTypeColumn}," +
			$"{AbsorbanceRatioColumn}," +
			$"{StandardConfirmationIndexColumn}," +
			$"{StandardConfirmationPassedColumn}," +
			$"{StandardConfirmationErrorColumn}," +
			$"{WavelengthMaxColumn}," +
			$"{AbsorbanceAtWavelengthMaxColumn}," +
			$"{WavelengthMaxErrorColumn}," +
			$"{PeakPurityColumn}," +
			$"{PeakPurityPassedColumn}," +
			$"{PeakPurityErrorColumn}," +
			$"{DataSourceTypeColumn}," +
			$"{AbsorbanceRatioErrorColumn}," +
			$"{ManuallyOverridenColumn}," +
			$"{MidIndexColumn}," +
			$"{StartIndexColumn}," +
			$"{StopIndexColumn}," +
			$"{LibraryCompoundColumn}," +
			$"{SearchLibraryCompoundColumn}," +
			$"{LibraryNameColumn}," +
			$"{SearchLibraryColumn}," +
			$"{LibraryGuidColumn}," +
			$"{LibraryConfirmationColumn}," +
			$"{HitQualityValueColumn}," +
			$"{SearchMatchColumn}," +
			$"{CompoundAssignmentTypeColumn} " +
			$"FROM {TableName} ";
		public IList<RunPeakResult> GetRunPeakByCalculatedChannelDataId(IDbConnection connection, long calculatedChannelDataId)
        {
            try
            {
               return connection.Query<RunPeakResult>(
                    SelectSql + $" WHERE {CalculatedChannelDataIdColumn} = {calculatedChannelDataId}").ToList();

            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetRunPeakByCalculatedChannelDataId method", ex);
                throw;
            }
        }

        
        public void SaveRunPeakResult(IDbConnection connection, RunPeakResult runPeakResultEntity)
        {
            try
            {
                runPeakResultEntity.Id = connection.ExecuteScalar<long>(InsertSql + " RETURNING Id", runPeakResultEntity);
            }
            catch (Exception exception)
            {
                Log.Error(
                    $"Error occured in SaveRunPeakResult() method of class{GetType().Name} - {exception.Message}");
                throw;
            }
        }

        public void SaveRunPeakResult(IDbConnection connection, IList<RunPeakResult> runPeakResultEntities)
        {
	        try
	        {
		        using (var insertBatch = new NpgsqlCommand(InsertSql, (NpgsqlConnection) connection))
		        {
			        insertBatch.Parameters.Add($"{CalculatedChannelDataIdColumn}", NpgsqlDbType.Bigint);
			        insertBatch.Parameters.Add($"{BatchRunChannelGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{AmountColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{AreaColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{AreaPercentColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{AreaToAmountRatioColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{AreaToHeightRatioColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{BaselineCodeColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{BaselineInterceptColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{BaselineSlopeColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{CalibrationInRangeColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{ChannelGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{CompoundGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{EndPeakTimeColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ExpoAColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ExpoBColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ExpoCorrectionColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ExpoDecayColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ExpoHeightColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{HeightColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{InternalStandardAmountRatioColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{InternalStandardAreaRatioColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{InternalStandardGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{InternalStandardHeightRatioColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{IsBaselineExpoColumn}", NpgsqlDbType.Boolean);
			        insertBatch.Parameters.Add($"{KPrimeColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{NormalizedAmountColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{OverlappedColumn}", NpgsqlDbType.Boolean);
			        insertBatch.Parameters.Add($"{PeakGroupColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{PeakGroupGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{PeakGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{PeakNameColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{PeakNumberColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{PeakWidthAt10PercentHeightColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{PeakWidthAt5PercentHeightColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{PeakWidthAtHalfHeightColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{PeakWidthColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{PlatesDorseyFoleyColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{PlatesTangentialColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{RawAmountColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{RelativeRetentionTimeColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{RelativeRetTimeSuitColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ResolutionColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{RetentionTimeColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{RetTimeReferenceGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{RrtReferenceGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{SignalColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{SignalToNoiseRatioColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{StartPeakTimeColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{TailingFactorColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{InternalStandardAmountColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{ReferenceInternalStandardCompoundGuidColumn}",
				        NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{AmountErrorColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{CompoundTypeColumn}", NpgsqlDbType.Smallint);
			        insertBatch.Parameters.Add($"{AbsorbanceRatioColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{StandardConfirmationIndexColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{StandardConfirmationPassedColumn}",
				        NpgsqlDbType.Boolean);
			        insertBatch.Parameters.Add($"{StandardConfirmationErrorColumn}",
				        NpgsqlDbType.Smallint);
			        insertBatch.Parameters.Add($"{WavelengthMaxColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{AbsorbanceAtWavelengthMaxColumn}",
				        NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{WavelengthMaxErrorColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{PeakPurityColumn}", NpgsqlDbType.Double);
			        insertBatch.Parameters.Add($"{PeakPurityPassedColumn}", NpgsqlDbType.Boolean);
			        insertBatch.Parameters.Add($"{PeakPurityErrorColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{DataSourceTypeColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{AbsorbanceRatioErrorColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{ManuallyOverridenColumn}", NpgsqlDbType.Boolean);
			        insertBatch.Parameters.Add($"{MidIndexColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{StartIndexColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{StopIndexColumn}", NpgsqlDbType.Integer);
			        insertBatch.Parameters.Add($"{LibraryNameColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{SearchLibraryColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{LibraryGuidColumn}", NpgsqlDbType.Uuid);
			        insertBatch.Parameters.Add($"{LibraryCompoundColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{SearchLibraryCompoundColumn}", NpgsqlDbType.Varchar,
				        256);
			        insertBatch.Parameters.Add($"{LibraryConfirmationColumn}", NpgsqlDbType.Boolean);
			        insertBatch.Parameters.Add($"{HitQualityValueColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{SearchMatchColumn}", NpgsqlDbType.Varchar, 256);
			        insertBatch.Parameters.Add($"{CompoundAssignmentTypeColumn}",
				        NpgsqlDbType.Smallint);

			        insertBatch.Prepare();

			        foreach (var runPeakResultEntity in runPeakResultEntities)
			        {
				        insertBatch.Parameters[$"{CalculatedChannelDataIdColumn}"].Value =
					        runPeakResultEntity.CalculatedChannelDataId;
						insertBatch.Parameters[$"{BatchRunChannelGuidColumn}"].Value =
							runPeakResultEntity.BatchRunChannelGuid;
						insertBatch.Parameters[$"{AmountColumn}"].Value =
							runPeakResultEntity.Amount ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{AreaColumn}"].Value = runPeakResultEntity.Area;
						insertBatch.Parameters[$"{AreaPercentColumn}"].Value =
							runPeakResultEntity.AreaPercent;
						insertBatch.Parameters[$"{AreaToAmountRatioColumn}"].Value =
							runPeakResultEntity.AreaToAmountRatio ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{AreaToHeightRatioColumn}"].Value =
							runPeakResultEntity.AreaToHeightRatio ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{BaselineCodeColumn}"].Value =
							runPeakResultEntity.BaselineCode;
						insertBatch.Parameters[$"{BaselineInterceptColumn}"].Value =
							runPeakResultEntity.BaselineIntercept;
						insertBatch.Parameters[$"{BaselineSlopeColumn}"].Value =
							runPeakResultEntity.BaselineSlope;
						insertBatch.Parameters[$"{CalibrationInRangeColumn}"].Value =
							runPeakResultEntity.CalibrationInRange ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{ChannelGuidColumn}"].Value =
							runPeakResultEntity.ChannelGuid;
						insertBatch.Parameters[$"{CompoundGuidColumn}"].Value =
							runPeakResultEntity.CompoundGuid;
						insertBatch.Parameters[$"{EndPeakTimeColumn}"].Value =
							runPeakResultEntity.EndPeakTime;
						insertBatch.Parameters[$"{ExpoAColumn}"].Value = runPeakResultEntity.ExpoA;
						insertBatch.Parameters[$"{ExpoBColumn}"].Value = runPeakResultEntity.ExpoB;
						insertBatch.Parameters[$"{ExpoCorrectionColumn}"].Value =
							runPeakResultEntity.ExpoCorrection;
						insertBatch.Parameters[$"{ExpoDecayColumn}"].Value = runPeakResultEntity.ExpoDecay;
						insertBatch.Parameters[$"{ExpoHeightColumn}"].Value =
							runPeakResultEntity.ExpoHeight;
						insertBatch.Parameters[$"{HeightColumn}"].Value = runPeakResultEntity.Height;
						insertBatch.Parameters[$"{InternalStandardAmountRatioColumn}"].Value =
							runPeakResultEntity.InternalStandardAmountRatio ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{InternalStandardAreaRatioColumn}"].Value =
							runPeakResultEntity.InternalStandardAreaRatio ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{InternalStandardGuidColumn}"].Value =
							runPeakResultEntity.InternalStandardGuid;
						insertBatch.Parameters[$"{InternalStandardHeightRatioColumn}"].Value =
							runPeakResultEntity.InternalStandardHeightRatio ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{IsBaselineExpoColumn}"].Value =
							runPeakResultEntity.IsBaselineExpo;
						insertBatch.Parameters[$"{KPrimeColumn}"].Value = runPeakResultEntity.KPrime;
						insertBatch.Parameters[$"{NormalizedAmountColumn}"].Value =
							runPeakResultEntity.NormalizedAmount ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{OverlappedColumn}"].Value =
							runPeakResultEntity.Overlapped;
						insertBatch.Parameters[$"{PeakGroupColumn}"].Value =
							runPeakResultEntity.PeakGroup ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{PeakGroupGuidColumn}"].Value =
							runPeakResultEntity.PeakGroupGuid;
						insertBatch.Parameters[$"{PeakGuidColumn}"].Value = runPeakResultEntity.PeakGuid;
						insertBatch.Parameters[$"{PeakNameColumn}"].Value =
							runPeakResultEntity.PeakName ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{PeakNumberColumn}"].Value =
							runPeakResultEntity.PeakNumber;
						insertBatch.Parameters[$"{PeakWidthAt10PercentHeightColumn}"].Value =
							runPeakResultEntity.PeakWidthAt10PercentHeight;
						insertBatch.Parameters[$"{PeakWidthAt5PercentHeightColumn}"].Value =
							runPeakResultEntity.PeakWidthAt5PercentHeight;
						insertBatch.Parameters[$"{PeakWidthAtHalfHeightColumn}"].Value =
							runPeakResultEntity.PeakWidthAtHalfHeight;
						insertBatch.Parameters[$"{PeakWidthColumn}"].Value = runPeakResultEntity.PeakWidth;
						insertBatch.Parameters[$"{PlatesDorseyFoleyColumn}"].Value =
							runPeakResultEntity.PlatesDorseyFoley;
						insertBatch.Parameters[$"{PlatesTangentialColumn}"].Value =
							runPeakResultEntity.PlatesTangential;
						insertBatch.Parameters[$"{RawAmountColumn}"].Value =
							runPeakResultEntity.RawAmount ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{RelativeRetentionTimeColumn}"].Value =
							runPeakResultEntity.RelativeRetentionTime ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{RelativeRetTimeSuitColumn}"].Value =
							runPeakResultEntity.RelativeRetTimeSuit;
						insertBatch.Parameters[$"{ResolutionColumn}"].Value =
							runPeakResultEntity.Resolution;
						insertBatch.Parameters[$"{RetentionTimeColumn}"].Value =
							runPeakResultEntity.RetentionTime;
						insertBatch.Parameters[$"{RetTimeReferenceGuidColumn}"].Value =
							runPeakResultEntity.RetTimeReferenceGuid;
						insertBatch.Parameters[$"{RrtReferenceGuidColumn}"].Value =
							runPeakResultEntity.RrtReferenceGuid;
						insertBatch.Parameters[$"{SignalColumn}"].Value = runPeakResultEntity.Signal;
						insertBatch.Parameters[$"{SignalToNoiseRatioColumn}"].Value =
							runPeakResultEntity.SignalToNoiseRatio;
						insertBatch.Parameters[$"{StartPeakTimeColumn}"].Value =
							runPeakResultEntity.StartPeakTime;
						insertBatch.Parameters[$"{TailingFactorColumn}"].Value =
							runPeakResultEntity.TailingFactor;
						insertBatch.Parameters[$"{InternalStandardAmountColumn}"].Value =
							runPeakResultEntity.InternalStandardAmount ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{ReferenceInternalStandardCompoundGuidColumn}"].Value =
							runPeakResultEntity.ReferenceInternalStandardCompoundGuid;
						insertBatch.Parameters[$"{AmountErrorColumn}"].Value =
							runPeakResultEntity.AmountError;
						insertBatch.Parameters[$"{CompoundTypeColumn}"].Value =
							runPeakResultEntity.CompoundType;
						insertBatch.Parameters[$"{AbsorbanceRatioColumn}"].Value =
							runPeakResultEntity.AbsorbanceRatio ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{StandardConfirmationIndexColumn}"].Value =
							runPeakResultEntity.StandardConfirmationIndex ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{StandardConfirmationPassedColumn}"].Value =
							runPeakResultEntity.StandardConfirmationPassed;
						insertBatch.Parameters[$"{StandardConfirmationErrorColumn}"].Value =
							runPeakResultEntity.StandardConfirmationError;
						insertBatch.Parameters[$"{WavelengthMaxColumn}"].Value =
							runPeakResultEntity.WavelengthMax ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{AbsorbanceAtWavelengthMaxColumn}"].Value =
							runPeakResultEntity.AbsorbanceAtWavelengthMax ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{WavelengthMaxErrorColumn}"].Value =
							runPeakResultEntity.WavelengthMaxError;
						insertBatch.Parameters[$"{PeakPurityColumn}"].Value =
							runPeakResultEntity.PeakPurity ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{PeakPurityPassedColumn}"].Value =
							runPeakResultEntity.PeakPurityPassed;
						insertBatch.Parameters[$"{PeakPurityErrorColumn}"].Value =
							runPeakResultEntity.PeakPurityError;
						insertBatch.Parameters[$"{DataSourceTypeColumn}"].Value =
							runPeakResultEntity.DataSourceType ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{AbsorbanceRatioErrorColumn}"].Value =
							runPeakResultEntity.AbsorbanceRatioError;
						insertBatch.Parameters[$"{ManuallyOverridenColumn}"].Value =
							runPeakResultEntity.ManuallyOverriden;
						insertBatch.Parameters[$"{MidIndexColumn}"].Value = runPeakResultEntity.MidIndex;
						insertBatch.Parameters[$"{StartIndexColumn}"].Value =
							runPeakResultEntity.StartIndex;
						insertBatch.Parameters[$"{StopIndexColumn}"].Value = runPeakResultEntity.StopIndex;
						insertBatch.Parameters[$"{LibraryNameColumn}"].Value =
							runPeakResultEntity.LibraryName ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{SearchLibraryColumn}"].Value =
							runPeakResultEntity.SearchLibrary ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{LibraryGuidColumn}"].Value =
							runPeakResultEntity.LibraryGuid;
						insertBatch.Parameters[$"{LibraryCompoundColumn}"].Value =
							runPeakResultEntity.LibraryCompound ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{SearchLibraryCompoundColumn}"].Value =
							runPeakResultEntity.SearchLibraryCompound ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{LibraryConfirmationColumn}"].Value =
							runPeakResultEntity.LibraryConfirmation;
						insertBatch.Parameters[$"{HitQualityValueColumn}"].Value =
							runPeakResultEntity.HitQualityValue ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{SearchMatchColumn}"].Value =
							runPeakResultEntity.SearchMatch ?? (object)DBNull.Value;
						insertBatch.Parameters[$"{CompoundAssignmentTypeColumn}"].Value =
							runPeakResultEntity.CompoundAssignmentType;
					
						insertBatch.ExecuteNonQuery();
					}
				}

			}
	        catch (Exception ex)
	        {
		        Log.Error("Error occurred in SaveRunPeakResult() method", ex);
		        throw;
	        }
        }
		public bool RemoveRunPeakResult(IDbConnection connection, long calculatedChannelDataId)
        {
            try
            {
                connection.Execute(
                    $"Delete from {TableName} where {CalculatedChannelDataIdColumn} = {calculatedChannelDataId} ");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error in RemoveRunPeakResult method", ex);
                throw;
            }
        }
        
    }
}
