using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class SuitabilityParametersDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "SuitabilityParameters";
		internal static string IdColumn { get; } = "Id";
		internal static string ChannelMethodIdColumn { get; } = "ChannelMethodId";
		internal static string ComplianceStandardColumn { get; } = "ComplianceStandard";
		internal static string EfficiencyReportingColumn { get; } = "EfficiencyReporting";
		internal static string ColumnLengthColumn { get; } = "ColumnLength";
		internal static string SignalToNoiseStartTimeColumn { get; } = "SignalToNoiseStartTime";
		internal static string SignalToNoiseEndTimeColumn { get; } = "SignalToNoiseEndTime";
		internal static string NumberOfSigmasColumn { get; } = "NumberOfSigmas";
		internal static string AnalyzeModeColumn { get; } = "AnalyzeMode";
		internal static string TailingFactorCalculationColumn { get; } = "TailingFactorCalculation";
		internal static string AreaLimitIsUsedColumn { get; } = "AreaLimitIsUsed";
		internal static string AreaLimitLowerLimitColumn { get; } = "AreaLimitLowerLimit";
		internal static string AreaLimitUpperLimitColumn { get; } = "AreaLimitUpperLimit";
		internal static string AreaLimitRelativeStDevPercentColumn { get; } = "AreaLimitRelativeStDevPercent";
		internal static string HeightLimitIsUsedColumn { get; } = "HeightLimitIsUsed";
		internal static string HeightLimitLowerLimitColumn { get; } = "HeightLimitLowerLimit";
		internal static string HeightLimitUpperLimitColumn { get; } = "HeightLimitUpperLimit";
		internal static string HeightLimitRelativeStDevPercentColumn { get; } = "HeightLimitRelativeStDevPercent";
		internal static string NTanLimitIsUsedColumn { get; } = "NTanLimitIsUsed";
		internal static string NTanLimitLowerLimitColumn { get; } = "NTanLimitLowerLimit";
		internal static string NTanLimitUpperLimitColumn { get; } = "NTanLimitUpperLimit";
		internal static string NTanLimitRelativeStDevPercentColumn { get; } = "NTanLimitRelativeStDevPercent";
		internal static string NFoleyLimitIsUsedColumn { get; } = "NFoleyLimitIsUsed";
		internal static string NFoleyLimitLowerLimitColumn { get; } = "NFoleyLimitLowerLimit";
		internal static string NFoleyLimitUpperLimitColumn { get; } = "NFoleyLimitUpperLimit";
		internal static string NFoleyLimitRelativeStDevPercentColumn { get; } = "NFoleyLimitRelativeStDevPercent";


        internal static string NFoleyDorseyLimitIsUsedColumn { get; } = "NFoleyDorseyLimitIsUsed";
        internal static string NFoleyDorseyLimitLowerLimitColumn { get; } = "NFoleyDorseyLimitLowerLimit";
        internal static string NFoleyDorseyLimitUpperLimitColumn { get; } = "NFoleyDorseyLimitUpperLimit";
        internal static string NFoleyDorseyLimitRelativeStDevPercentColumn { get; } = "NFoleyDorseyLimitRelativeStDevPercent";


        internal static string TailingFactorSymmetryLimitIsUsedColumn { get; } = "TailingFactorSymmetryLimitIsUsed";
		internal static string TailingFactorSymmetryLimitLowerLimitColumn { get; } = "TailingFactorSymmetryLimitLowerLimit";
		internal static string TailingFactorSymmetryLimitUpperLimitColumn { get; } = "TailingFactorSymmetryLimitUpperLimit";
		internal static string TailingFactorSymmetryLimitRelativeStDevPercentColumn { get; } = "TailingFactorSymmetryLimitRelativeStDevPercent";
		internal static string UspResolutionLimitIsUsedColumn { get; } = "UspResolutionLimitIsUsed";
		internal static string UspResolutionLimitLowerLimitColumn { get; } = "UspResolutionLimitLowerLimit";
		internal static string UspResolutionLimitUpperLimitColumn { get; } = "UspResolutionLimitUpperLimit";
		internal static string UspResolutionLimitRelativeStDevPercentColumn { get; } = "UspResolutionLimitRelativeStDevPercent";
		internal static string KPrimeLimitIsUsedColumn { get; } = "KPrimeLimitIsUsed";
		internal static string KPrimeLimitLowerLimitColumn { get; } = "KPrimeLimitLowerLimit";
		internal static string KPrimeLimitUpperLimitColumn { get; } = "KPrimeLimitUpperLimit";
		internal static string KPrimeLimitRelativeStDevPercentColumn { get; } = "KPrimeLimitRelativeStDevPercent";
		internal static string ResolutionLimitIsUsedColumn { get; } = "ResolutionLimitIsUsed";
		internal static string ResolutionLimitLowerLimitColumn { get; } = "ResolutionLimitLowerLimit";
		internal static string ResolutionLimitUpperLimitColumn { get; } = "ResolutionLimitUpperLimit";
		internal static string ResolutionLimitRelativeStDevPercentColumn { get; } = "ResolutionLimitRelativeStDevPercent";
		internal static string AlphaLimitIsUsedColumn { get; } = "AlphaLimitIsUsed";
		internal static string AlphaLimitLowerLimitColumn { get; } = "AlphaLimitLowerLimit";
		internal static string AlphaLimitUpperLimitColumn { get; } = "AlphaLimitUpperLimit";
		internal static string AlphaLimitRelativeStDevPercentColumn { get; } = "AlphaLimitRelativeStDevPercent";
		internal static string SignalToNoiseLimitIsUsedColumn { get; } = "SignalToNoiseLimitIsUsed";
		internal static string SignalToNoiseLimitLowerLimitColumn { get; } = "SignalToNoiseLimitLowerLimit";
		internal static string SignalToNoiseLimitUpperLimitColumn { get; } = "SignalToNoiseLimitUpperLimit";
		internal static string SignalToNoiseLimitRelativeStDevPercentColumn { get; } = "SignalToNoiseLimitRelativeStDevPercent";
		internal static string PeakWidthLimitIsUsedColumn { get; } = "PeakWidthLimitIsUsed";
		internal static string PeakWidthLimitLowerLimitColumn { get; } = "PeakWidthLimitLowerLimit";
		internal static string PeakWidthLimitUpperLimitColumn { get; } = "PeakWidthLimitUpperLimit";
		internal static string PeakWidthLimitRelativeStDevPercentColumn { get; } = "PeakWidthLimitRelativeStDevPercent";

		protected readonly string Select =
			$"SELECT {TableName}.{IdColumn}," +
			$"{TableName}.{ChannelMethodIdColumn}," +
			$"{TableName}.{ComplianceStandardColumn}," +
			$"{TableName}.{EfficiencyReportingColumn}," +
			$"{TableName}.{ColumnLengthColumn}," +
			$"{TableName}.{SignalToNoiseStartTimeColumn}," +
			$"{TableName}.{SignalToNoiseEndTimeColumn}," +
			$"{TableName}.{NumberOfSigmasColumn}," +
			$"{TableName}.{AnalyzeModeColumn}," +
			$"{TableName}.{TailingFactorCalculationColumn}," +
			$"{TableName}.{AreaLimitIsUsedColumn}," +
			$"{TableName}.{AreaLimitLowerLimitColumn}," +
			$"{TableName}.{AreaLimitUpperLimitColumn}," +
			$"{TableName}.{AreaLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{HeightLimitIsUsedColumn}," +
			$"{TableName}.{HeightLimitLowerLimitColumn}," +
			$"{TableName}.{HeightLimitUpperLimitColumn}," +
			$"{TableName}.{HeightLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{NTanLimitIsUsedColumn}," +
			$"{TableName}.{NTanLimitLowerLimitColumn}," +
			$"{TableName}.{NTanLimitUpperLimitColumn}," +
			$"{TableName}.{NTanLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{NFoleyLimitIsUsedColumn}," +
			$"{TableName}.{NFoleyLimitLowerLimitColumn}," +
			$"{TableName}.{NFoleyLimitUpperLimitColumn}," +
			$"{TableName}.{NFoleyLimitRelativeStDevPercentColumn}," +
            $"{TableName}.{NFoleyDorseyLimitIsUsedColumn}," +
            $"{TableName}.{NFoleyDorseyLimitLowerLimitColumn}," +
            $"{TableName}.{NFoleyDorseyLimitUpperLimitColumn}," +
            $"{TableName}.{NFoleyDorseyLimitRelativeStDevPercentColumn}," +
            $"{TableName}.{TailingFactorSymmetryLimitIsUsedColumn}," +
			$"{TableName}.{TailingFactorSymmetryLimitLowerLimitColumn}," +
			$"{TableName}.{TailingFactorSymmetryLimitUpperLimitColumn}," +
			$"{TableName}.{TailingFactorSymmetryLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{UspResolutionLimitIsUsedColumn}," +
			$"{TableName}.{UspResolutionLimitLowerLimitColumn}," +
			$"{TableName}.{UspResolutionLimitUpperLimitColumn}," +
			$"{TableName}.{UspResolutionLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{KPrimeLimitIsUsedColumn}," +
			$"{TableName}.{KPrimeLimitLowerLimitColumn}," +
			$"{TableName}.{KPrimeLimitUpperLimitColumn}," +
			$"{TableName}.{KPrimeLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{ResolutionLimitIsUsedColumn}," +
			$"{TableName}.{ResolutionLimitLowerLimitColumn}," +
			$"{TableName}.{ResolutionLimitUpperLimitColumn}," +
			$"{TableName}.{ResolutionLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{AlphaLimitIsUsedColumn}," +
			$"{TableName}.{AlphaLimitLowerLimitColumn}," +
			$"{TableName}.{AlphaLimitUpperLimitColumn}," +
			$"{TableName}.{AlphaLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{SignalToNoiseLimitIsUsedColumn}," +
			$"{TableName}.{SignalToNoiseLimitLowerLimitColumn}," +
			$"{TableName}.{SignalToNoiseLimitUpperLimitColumn}," +
			$"{TableName}.{SignalToNoiseLimitRelativeStDevPercentColumn}," +
			$"{TableName}.{PeakWidthLimitIsUsedColumn}," +
			$"{TableName}.{PeakWidthLimitLowerLimitColumn}," +
			$"{TableName}.{PeakWidthLimitUpperLimitColumn}," +
			$"{TableName}.{PeakWidthLimitRelativeStDevPercentColumn} " +
			$"FROM {TableName} ";

		protected readonly string InsertInto =
			$"INSERT INTO {TableName} " +
			"(" +
			$"{ChannelMethodIdColumn}," +
			$"{ComplianceStandardColumn}," +
			$"{EfficiencyReportingColumn}," +
			$"{ColumnLengthColumn}," +
			$"{SignalToNoiseStartTimeColumn}," +
			$"{SignalToNoiseEndTimeColumn}," +
			$"{NumberOfSigmasColumn}," +
			$"{AnalyzeModeColumn}," +
			$"{TailingFactorCalculationColumn}," +
			$"{AreaLimitIsUsedColumn}," +
			$"{AreaLimitLowerLimitColumn}," +
			$"{AreaLimitUpperLimitColumn}," +
			$"{AreaLimitRelativeStDevPercentColumn}," +
			$"{HeightLimitIsUsedColumn}," +
			$"{HeightLimitLowerLimitColumn}," +
			$"{HeightLimitUpperLimitColumn}," +
			$"{HeightLimitRelativeStDevPercentColumn}," +
			$"{NTanLimitIsUsedColumn}," +
			$"{NTanLimitLowerLimitColumn}," +
			$"{NTanLimitUpperLimitColumn}," +
			$"{NTanLimitRelativeStDevPercentColumn}," +
			$"{NFoleyLimitIsUsedColumn}," +
			$"{NFoleyLimitLowerLimitColumn}," +
			$"{NFoleyLimitUpperLimitColumn}," +
			$"{NFoleyLimitRelativeStDevPercentColumn}," +
            $"{NFoleyDorseyLimitIsUsedColumn}," +
            $"{NFoleyDorseyLimitLowerLimitColumn}," +
            $"{NFoleyDorseyLimitUpperLimitColumn}," +
            $"{NFoleyDorseyLimitRelativeStDevPercentColumn}," +
            $"{TailingFactorSymmetryLimitIsUsedColumn}," +
			$"{TailingFactorSymmetryLimitLowerLimitColumn}," +
			$"{TailingFactorSymmetryLimitUpperLimitColumn}," +
			$"{TailingFactorSymmetryLimitRelativeStDevPercentColumn}," +
			$"{UspResolutionLimitIsUsedColumn}," +
			$"{UspResolutionLimitLowerLimitColumn}," +
			$"{UspResolutionLimitUpperLimitColumn}," +
			$"{UspResolutionLimitRelativeStDevPercentColumn}," +
			$"{KPrimeLimitIsUsedColumn}," +
			$"{KPrimeLimitLowerLimitColumn}," +
			$"{KPrimeLimitUpperLimitColumn}," +
			$"{KPrimeLimitRelativeStDevPercentColumn}," +
			$"{ResolutionLimitIsUsedColumn}," +
			$"{ResolutionLimitLowerLimitColumn}," +
			$"{ResolutionLimitUpperLimitColumn}," +
			$"{ResolutionLimitRelativeStDevPercentColumn}," +
			$"{AlphaLimitIsUsedColumn}," +
			$"{AlphaLimitLowerLimitColumn}," +
			$"{AlphaLimitUpperLimitColumn}," +
			$"{AlphaLimitRelativeStDevPercentColumn}," +
			$"{SignalToNoiseLimitIsUsedColumn}," +
			$"{SignalToNoiseLimitLowerLimitColumn}," +
			$"{SignalToNoiseLimitUpperLimitColumn}," +
			$"{SignalToNoiseLimitRelativeStDevPercentColumn}," +
			$"{PeakWidthLimitIsUsedColumn}," +
			$"{PeakWidthLimitLowerLimitColumn}," +
			$"{PeakWidthLimitUpperLimitColumn}," +
			$"{PeakWidthLimitRelativeStDevPercentColumn}" +
			")" +
			"VALUES (" +
			$"@{ChannelMethodIdColumn}," +
			$"@{ComplianceStandardColumn}," +
			$"@{EfficiencyReportingColumn}," +
			$"@{ColumnLengthColumn}," +
			$"@{SignalToNoiseStartTimeColumn}," +
			$"@{SignalToNoiseEndTimeColumn}," +
			$"@{NumberOfSigmasColumn}," +
			$"@{AnalyzeModeColumn}," +
			$"@{TailingFactorCalculationColumn}," +
			$"@{AreaLimitIsUsedColumn}," +
			$"@{AreaLimitLowerLimitColumn}," +
			$"@{AreaLimitUpperLimitColumn}," +
			$"@{AreaLimitRelativeStDevPercentColumn}," +
			$"@{HeightLimitIsUsedColumn}," +
			$"@{HeightLimitLowerLimitColumn}," +
			$"@{HeightLimitUpperLimitColumn}," +
			$"@{HeightLimitRelativeStDevPercentColumn}," +
			$"@{NTanLimitIsUsedColumn}," +
			$"@{NTanLimitLowerLimitColumn}," +
			$"@{NTanLimitUpperLimitColumn}," +
			$"@{NTanLimitRelativeStDevPercentColumn}," +
			$"@{NFoleyLimitIsUsedColumn}," +
			$"@{NFoleyLimitLowerLimitColumn}," +
			$"@{NFoleyLimitUpperLimitColumn}," +
			$"@{NFoleyLimitRelativeStDevPercentColumn}," +
            $"@{NFoleyDorseyLimitIsUsedColumn}," +
            $"@{NFoleyDorseyLimitLowerLimitColumn}," +
            $"@{NFoleyDorseyLimitUpperLimitColumn}," +
            $"@{NFoleyDorseyLimitRelativeStDevPercentColumn}," +
            $"@{TailingFactorSymmetryLimitIsUsedColumn}," +
			$"@{TailingFactorSymmetryLimitLowerLimitColumn}," +
			$"@{TailingFactorSymmetryLimitUpperLimitColumn}," +
			$"@{TailingFactorSymmetryLimitRelativeStDevPercentColumn}," +
			$"@{UspResolutionLimitIsUsedColumn}," +
			$"@{UspResolutionLimitLowerLimitColumn}," +
			$"@{UspResolutionLimitUpperLimitColumn}," +
			$"@{UspResolutionLimitRelativeStDevPercentColumn}," +
			$"@{KPrimeLimitIsUsedColumn}," +
			$"@{KPrimeLimitLowerLimitColumn}," +
			$"@{KPrimeLimitUpperLimitColumn}," +
			$"@{KPrimeLimitRelativeStDevPercentColumn}," +
			$"@{ResolutionLimitIsUsedColumn}," +
			$"@{ResolutionLimitLowerLimitColumn}," +
			$"@{ResolutionLimitUpperLimitColumn}," +
			$"@{ResolutionLimitRelativeStDevPercentColumn}," +
			$"@{AlphaLimitIsUsedColumn}," +
			$"@{AlphaLimitLowerLimitColumn}," +
			$"@{AlphaLimitUpperLimitColumn}," +
			$"@{AlphaLimitRelativeStDevPercentColumn}," +
			$"@{SignalToNoiseLimitIsUsedColumn}," +
			$"@{SignalToNoiseLimitLowerLimitColumn}," +
			$"@{SignalToNoiseLimitUpperLimitColumn}," +
			$"@{SignalToNoiseLimitRelativeStDevPercentColumn}," +
			$"@{PeakWidthLimitIsUsedColumn}," +
			$"@{PeakWidthLimitLowerLimitColumn}," +
			$"@{PeakWidthLimitUpperLimitColumn}," +
			$"@{PeakWidthLimitRelativeStDevPercentColumn}) ";
		public virtual bool Create(IDbConnection connection, SuitabilityParameters suitabilityParameters)
		{
			try
			{
				suitabilityParameters.Id = connection.ExecuteScalar<long>(InsertInto + "RETURNING Id", suitabilityParameters);
				return (suitabilityParameters.Id != 0);
			}
			catch (Exception exception)
			{
				Log.Error(
					$"Error occured in Create() method of class{GetType().Name} - {exception.Message}");
				throw;
			}
		}

		public SuitabilityParameters GetSuitabilityParametersByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<SuitabilityParameters>(
					Select +
					$"WHERE {ChannelMethodIdColumn} = @ChannelMethodId",
					new { ChannelMethodId = channelMethodId});
			}
			catch (Exception exception)
			{
				Log.Error(
					$"Error occured in GetSuitabilityParametersByChannelMethodId() method of class{GetType().Name} - {exception.Message}");
				throw;
			}
		}
		public void Delete(IDbConnection connection, long channelMethodId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} WHERE {ChannelMethodIdColumn} = {channelMethodId} ");
			}
			catch (Exception exception)
			{
				Log.Error(
					$"Error occured in Delete() method of class{GetType().Name} - {exception.Message}");
				throw;
			}
		}
	}
}
