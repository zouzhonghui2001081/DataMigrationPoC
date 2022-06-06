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
	internal class CompoundDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static string TableName { get; } = "Compound";
		internal static string IdColumn { get; } = "Id";
        public static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		internal static string GuidColumn { get; } = "Guid";
		internal static string CompoundNumberColumn { get; } = "CompoundNumber";
		internal static string NameColumn { get; } = "Name";
		internal static string ProcessingMethodChannelGuidColumn { get; } = "ProcessingMethodChannelGuid";
		internal static string CompoundTypeColumn { get; } = "CompoundType";

		internal static string ExpectedRetentionTimeColumn { get; } = "ExpectedRetentionTime";
		internal static string RetentionTimeWindowAbsoluteColumn { get; } = "RetentionTimeWindowAbsolute";
		internal static string RetentionTimeWindowInPercentsColumn { get; } = "RetentionTimeWindowInPercents";
		internal static string RetTimeWindowStartColumn { get; } = "RetTimeWindowStart";
		internal static string RetTimeWindowEndColumn { get; } = "RetTimeWindowEnd";
		internal static string IsRetTimeReferencePeakColumn { get; } = "IsRetTimeReferencePeak";
		internal static string RetTimeReferencePeakGuidColumn { get; } = "RetTimeReferencePeakGuid";
		internal static string RetentionIndexColumn { get; } = "RetentionIndex";
		internal static string UseClosestPeakColumn { get; } = "UseClosestPeak";
		internal static string IsIntStdReferencePeakColumn { get; } = "IsIntStdReferencePeak";
		internal static string IntStdReferenceGuidColumn { get; } = "IntStdReferenceGuid";
		internal static string IndexColumn { get; } = "Index";
		internal static string IsRrtReferencePeakColumn { get; } = "IsRrtReferencePeak";
		internal static string InternalStandardColumn { get; } = "InternalStandard";
		internal static string ReferenceInternalStandardGuidColumn { get; } = "ReferenceInternalStandardGuid";
		internal static string InternalStandardAmountColumn { get; } = "InternalStandardAmount";
		internal static string PurityColumn { get; } = "Purity";
		internal static string QuantifyUsingAreaColumn { get; } = "QuantifyUsingArea";
		internal static string CalibrationTypeColumn { get; } = "CalibrationType";
		internal static string WeightingTypeColumn { get; } = "WeightingType";
		internal static string ScalingColumn { get; } = "Scaling";
		internal static string OriginTreatmentColumn { get; } = "OriginTreatment";
		internal static string CalibrationFactorColumn { get; } = "CalibrationFactor";
		internal static string ReferenceCompoundGuidColumn { get; } = "ReferenceCompoundGuid";
		internal static string IsCompoundGroupColumn { get; } = "IsCompoundGroup";
		internal static string StartTimeColumn { get; } = "StartTime";
		internal static string EndTimeColumn { get; } = "EndTime";
		internal static string UsedForSuitabilityColumn { get; } = "UsedForSuitability";

		

		protected readonly string SelectSql = "SELECT " +
		                                      $"{IdColumn}," +
		                                      $"{ProcessingMethodIdColumn}," +
		                                      $"{GuidColumn}," +
		                                      $"{CompoundNumberColumn}," +
		                                      $"{NameColumn}," +
		                                      $"{ProcessingMethodChannelGuidColumn}," +
		                                      $"{CompoundTypeColumn}," +
		                                      $"{ExpectedRetentionTimeColumn}," +
		                                      $"{RetentionTimeWindowAbsoluteColumn}," +
		                                      $"{RetentionTimeWindowInPercentsColumn}," +
		                                      $"{RetTimeWindowStartColumn}," +
		                                      $"{RetTimeWindowEndColumn}," +
		                                      $"{IsRetTimeReferencePeakColumn}," +
		                                      $"{RetTimeReferencePeakGuidColumn}," +
		                                      $"{RetentionIndexColumn}," +
		                                      $"{UseClosestPeakColumn}," +
		                                      $"{IsIntStdReferencePeakColumn}," +
		                                      $"{IntStdReferenceGuidColumn}," +
		                                      $"{IndexColumn}," +
		                                      $"{IsRrtReferencePeakColumn}," +
		                                      $"{InternalStandardColumn}," +
		                                      $"{ReferenceInternalStandardGuidColumn}," +
		                                      $"{InternalStandardAmountColumn}," +
		                                      $"{PurityColumn}," +
		                                      $"{QuantifyUsingAreaColumn}," +
		                                      $"{CalibrationTypeColumn}," +
		                                      $"{WeightingTypeColumn}," +
		                                      $"{ScalingColumn}," +
		                                      $"{OriginTreatmentColumn}," +
		                                      $"{CalibrationFactorColumn}," +
		                                      $"{ReferenceCompoundGuidColumn}," +
		                                      $"{IsCompoundGroupColumn}," +
		                                      $"{StartTimeColumn}," +
		                                      $"{EndTimeColumn}," +
		                                      $"{UsedForSuitabilityColumn} ";

		protected readonly string InsertSql = $"INSERT INTO {TableName} (" +
		                                      $"{ProcessingMethodIdColumn}," +
		                                      $"{GuidColumn}," +
		                                      $"{CompoundNumberColumn}," +
		                                      $"{NameColumn}," +
		                                      $"{ProcessingMethodChannelGuidColumn}," +
		                                      $"{CompoundTypeColumn}," +
		                                      $"{ExpectedRetentionTimeColumn}," +
		                                      $"{RetentionTimeWindowAbsoluteColumn}," +
		                                      $"{RetentionTimeWindowInPercentsColumn}," +
		                                      $"{RetTimeWindowStartColumn}," +
		                                      $"{RetTimeWindowEndColumn}," +
		                                      $"{IsRetTimeReferencePeakColumn}," +
		                                      $"{RetTimeReferencePeakGuidColumn}," +
		                                      $"{RetentionIndexColumn}," +
		                                      $"{UseClosestPeakColumn}," +
		                                      $"{IsIntStdReferencePeakColumn}," +
		                                      $"{IntStdReferenceGuidColumn}," +
		                                      $"{IndexColumn}," +
		                                      $"{IsRrtReferencePeakColumn}," +
		                                      $"{InternalStandardColumn}," +
		                                      $"{ReferenceInternalStandardGuidColumn}," +
		                                      $"{InternalStandardAmountColumn}," +
		                                      $"{PurityColumn}," +
		                                      $"{QuantifyUsingAreaColumn}," +
		                                      $"{CalibrationTypeColumn}," +
		                                      $"{WeightingTypeColumn}," +
		                                      $"{ScalingColumn}," +
		                                      $"{OriginTreatmentColumn}," +
		                                      $"{CalibrationFactorColumn}," +
		                                      $"{ReferenceCompoundGuidColumn}," +
		                                      $"{IsCompoundGroupColumn}," +
		                                      $"{StartTimeColumn}," +
		                                      $"{EndTimeColumn}," +
		                                      $"{UsedForSuitabilityColumn}) " +
		                                      "VALUES (" +
		                                      $"@{ProcessingMethodIdColumn}," +
		                                      $"@{GuidColumn}," +
		                                      $"@{CompoundNumberColumn}," +
		                                      $"@{NameColumn}," +
		                                      $"@{ProcessingMethodChannelGuidColumn}," +
		                                      $"@{CompoundTypeColumn}," +
		                                      $"@{ExpectedRetentionTimeColumn}," +
		                                      $"@{RetentionTimeWindowAbsoluteColumn}," +
		                                      $"@{RetentionTimeWindowInPercentsColumn}," +
		                                      $"@{RetTimeWindowStartColumn}," +
		                                      $"@{RetTimeWindowEndColumn}," +
		                                      $"@{IsRetTimeReferencePeakColumn}," +
		                                      $"@{RetTimeReferencePeakGuidColumn}," +
		                                      $"@{RetentionIndexColumn}," +
		                                      $"@{UseClosestPeakColumn}," +
		                                      $"@{IsIntStdReferencePeakColumn}," +
		                                      $"@{IntStdReferenceGuidColumn}," +
		                                      $"@{IndexColumn}," +
		                                      $"@{IsRrtReferencePeakColumn}," +
		                                      $"@{InternalStandardColumn}," +
		                                      $"@{ReferenceInternalStandardGuidColumn}," +
		                                      $"@{InternalStandardAmountColumn}," +
		                                      $"@{PurityColumn}," +
		                                      $"@{QuantifyUsingAreaColumn}," +
		                                      $"@{CalibrationTypeColumn}," +
		                                      $"@{WeightingTypeColumn}," +
		                                      $"@{ScalingColumn}," +
		                                      $"@{OriginTreatmentColumn}," +
		                                      $"@{CalibrationFactorColumn}," +
		                                      $"@{ReferenceCompoundGuidColumn}," +
		                                      $"@{IsCompoundGroupColumn}," +
		                                      $"@{StartTimeColumn}," +
		                                      $"@{EndTimeColumn}," +
		                                      $"@{UsedForSuitabilityColumn}) ";

        public virtual void Create(IDbConnection connection, Compound compound)
		{
			Log.Debug($"Invoked Create(): compounds.Guid={compound.Guid}");
			try
			{
				compound.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", compound);
			}
			catch (Exception ex)
			{
				Log.Error("Error occurred in Create()", ex);
				throw;
			}
		}
		public List<Compound> GetCompoundsByProcessingMethodId(IDbConnection connection, long processingMethodId)
		{
			Log.Debug($"Invoked GetCompoundsByProcessingMethodId(): processingMethodMethodBatchResultId={processingMethodId}");

			try
			{
				var result = connection.Query<Compound>(
					SelectSql +
					$"FROM {TableName} " +
					$"WHERE {ProcessingMethodIdColumn} = {processingMethodId}"
				).ToList();

				Log.Debug($"{result.Count} records found in {TableName} table");
				return result;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetCompoundsByProcessingMethodId() with {ProcessingMethodIdColumn}={processingMethodId}", ex);
				throw;
			}
		}
		public Compound GetCompoundByCompoundId(IDbConnection connection, long compoundId)
		{
			try
			{
				var result = connection.Query<Compound>(
					SelectSql +
					$"FROM {TableName} " +
					$"WHERE {IdColumn} = {compoundId}"
				).FirstOrDefault();

				return result;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetCompoundByCompoundId()", ex);
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
				Log.Error($"Error occured in Delete()", ex);
				throw;
			}
		}
	}
}
