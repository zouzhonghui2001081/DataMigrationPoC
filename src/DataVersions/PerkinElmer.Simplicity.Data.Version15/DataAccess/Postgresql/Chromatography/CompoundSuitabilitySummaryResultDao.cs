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
	internal class CompoundSuitabilitySummaryResultDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "CompoundSuitabilitySummaryResult";
		internal static string IdColumn { get; } = "Id";
		internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
		internal static string CompoundGuidColumn { get; } = "CompoundGuid";
		internal static string AreaAverageColumn { get; } = "AreaAverage";
		internal static string AreaStDevColumn { get; } = "AreaStDev";
		internal static string AreaRelativeStdDevPercentColumn { get; } = "AreaRelativeStdDevPercent";
		internal static string AreaRelativeStdDevPassedColumn { get; } = "AreaRelativeStdDevPassed";
		internal static string AreaFailureReasonColumn { get; } = "AreaFailureReason";
		internal static string HeightAverageColumn { get; } = "HeightAverage";
		internal static string HeightStDevColumn { get; } = "HeightStDev";
		internal static string HeightRelativeStdDevPercentColumn { get; } = "HeightRelativeStdDevPercent";
		internal static string HeightRelativeStdDevPassedColumn { get; } = "HeightRelativeStdDevPassed";
		internal static string HeightFailureReasonColumn { get; } = "HeightFailureReason";
		internal static string TheoreticalPlatesNAverageColumn { get; } = "TheoreticalPlatesNAverage";
		internal static string TheoreticalPlatesNStDevColumn { get; } = "TheoreticalPlatesNStDev";
		internal static string TheoreticalPlatesNRelativeStdDevPercentColumn { get; } = "TheoreticalPlatesNRelativeStdDevPercent";
		internal static string TheoreticalPlatesNRelativeStdDevPassedColumn { get; } = "TheoreticalPlatesNRelativeStdDevPassed";
		internal static string TheoreticalPlatesNFailureReasonColumn { get; } = "TheoreticalPlatesNFailureReason";
		internal static string TheoreticalPlatesNTanAverageColumn { get; } = "TheoreticalPlatesNTanAverage";
		internal static string TheoreticalPlatesNTanStDevColumn { get; } = "TheoreticalPlatesNTanStDev";
		internal static string TheoreticalPlatesNTanRelativeStdDevPercentColumn { get; } = "TheoreticalPlatesNTanRelativeStdDevPercent";
		internal static string TheoreticalPlatesNTanRelativeStdDevPassedColumn { get; } = "TheoreticalPlatesNTanRelativeStdDevPassed";
		internal static string TheoreticalPlatesNTanFailureReasonColumn { get; } = "TheoreticalPlatesNTanFailureReason";
		internal static string TailingFactorSymmetryAverageColumn { get; } = "TailingFactorSymmetryAverage";
		internal static string TailingFactorSymmetryStDevColumn { get; } = "TailingFactorSymmetryStDev";
		internal static string TailingFactorSymmetryRelativeStdDevPercentColumn { get; } = "TailingFactorSymmetryRelativeStdDevPercent";
		internal static string TailingFactorSymmetryRelativeStdDevPassedColumn { get; } = "TailingFactorSymmetryRelativeStdDevPassed";
		internal static string TailingFactorSymmetryFailureReasonColumn { get; } = "TailingFactorSymmetryFailureReason";
		internal static string RelativeRetentionAverageColumn { get; } = "RelativeRetentionAverage";
		internal static string RelativeRetentionStDevColumn { get; } = "RelativeRetentionStDev";
		internal static string RelativeRetentionRelativeStdDevPercentColumn { get; } = "RelativeRetentionRelativeStdDevPercent";
		internal static string RelativeRetentionRelativeStdDevPassedColumn { get; } = "RelativeRetentionRelativeStdDevPassed";
		internal static string RelativeRetentionFailureReasonColumn { get; } = "RelativeRetentionFailureReason";

        internal static string RelativeRetentionTimeAverageColumn { get; } = "RelativeRetentionTimeAverage";
        internal static string RelativeRetentionTimeStDevColumn { get; } = "RelativeRetentionTimeStDev";
        internal static string RelativeRetentionTimeRelativeStdDevPercentColumn { get; } = "RelativeRetentionTimeRelativeStdDevPercent";
        internal static string RelativeRetentionTimeRelativeStdDevPassedColumn { get; } = "RelativeRetentionTimeRelativeStdDevPassed";
        internal static string RelativeRetentionTimeFailureReasonColumn { get; } = "RelativeRetentionTimeFailureReason";

        internal static string RetentionTimeAverageColumn { get; } = "RetentionTimeAverage";
        internal static string RetentionTimeStDevColumn { get; } = "RetentionTimeStDev";
        internal static string RetentionTimeRelativeStdDevPercentColumn { get; } = "RetentionTimeRelativeStdDevPercent";
        internal static string RetentionTimeRelativeStdDevPassedColumn { get; } = "RetentionTimeRelativeStdDevPassed";
        internal static string RetentionTimeFailureReasonColumn { get; } = "RetentionTimeFailureReason";

        internal static string CapacityFactorKPrimeAverageColumn { get; } = "CapacityFactorKPrimeAverage";
		internal static string CapacityFactorKPrimeStDevColumn { get; } = "CapacityFactorKPrimeStDev";
		internal static string CapacityFactorKPrimeRelativeStdDevPercentColumn { get; } = "CapacityFactorKPrimeRelativeStdDevPercent";
		internal static string CapacityFactorKPrimeRelativeStdDevPassedColumn { get; } = "CapacityFactorKPrimeRelativeStdDevPassed";
		internal static string CapacityFactorKPrimeFailureReasonColumn { get; } = "CapacityFactorKPrimeFailureReason";
		internal static string ResolutionAverageColumn { get; } = "ResolutionAverage";
		internal static string ResolutionStDevColumn { get; } = "ResolutionStDev";
		internal static string ResolutionRelativeStdDevPercentColumn { get; } = "ResolutionRelativeStdDevPercent";
		internal static string ResolutionRelativeStdDevPassedColumn { get; } = "ResolutionRelativeStdDevPassed";
		internal static string ResolutionFailureReasonumn { get; } = "ResolutionFailureReason";
		internal static string UspResolutionAverageColumn { get; } = "UspResolutionAverage";
		internal static string UspResolutionStDevColumn { get; } = "UspResolutionStDev";
		internal static string UspResolutionRelativeStdDevPercentColumn { get; } = "UspResolutionRelativeStdDevPercent";
		internal static string UspResolutionRelativeStdDevPassedColumn { get; } = "UspResolutionRelativeStdDevPassed";
		internal static string UspResolutionFailureReasonColumn { get; } = "UspResolutionFailureReason";
		internal static string SignalToNoiseAverageColumn { get; } = "SignalToNoiseAverage";
		internal static string SignalToNoiseStDevColumn { get; } = "SignalToNoiseStDev";
		internal static string SignalToNoiseRelativeStdDevPercentColumn { get; } = "SignalToNoiseRelativeStdDevPercent";
		internal static string SignalToNoiseRelativeStdDevPassedColumn { get; } = "SignalToNoiseRelativeStdDevPassed";
		internal static string SignalToNoiseFailureReasonColumn { get; } = "SignalToNoiseFailureReason";
		internal static string PeakWidthAtBaseAverageColumn { get; } = "PeakWidthAtBaseAverage";
		internal static string PeakWidthAtBaseStDevColumn { get; } = "PeakWidthAtBaseStDev";
		internal static string PeakWidthAtBaseRelativeStdDevPercentColumn { get; } = "PeakWidthAtBaseRelativeStdDevPercent";
		internal static string PeakWidthAtBaseRelativeStdDevPassedColumn { get; } = "PeakWidthAtBaseRelativeStdDevPassed";
		internal static string PeakWidthAtBaseFailureReasonColumn { get; } = "PeakWidthAtBaseFailureReason";
		internal static string PeakWidthAt5PctAverageColumn { get; } = "PeakWidthAt5PctAverage";
		internal static string PeakWidthAt5PctStDevColumn { get; } = "PeakWidthAt5PctStDev";
		internal static string PeakWidthAt5PctRelativeStdDevPercentColumn { get; } = "PeakWidthAt5PctRelativeStdDevPercent";
		internal static string PeakWidthAt5PctRelativeStdDevPassedColumn { get; } = "PeakWidthAt5PctRelativeStdDevPassed";
		internal static string PeakWidthAt5PctFailureReasonColumn { get; } = "PeakWidthAt5PctFailureReason";
		internal static string PeakWidthAt10PctAverageColumn { get; } = "PeakWidthAt10PctAverage";
		internal static string PeakWidthAt10PctStDevColumn { get; } = "PeakWidthAt10PctStDev";
		internal static string PeakWidthAt10PctRelativeStdDevPercentColumn { get; } = "PeakWidthAt10PctRelativeStdDevPercent";
		internal static string PeakWidthAt10PctRelativeStdDevPassedColumn { get; } = "PeakWidthAt10PctRelativeStdDevPassed";
		internal static string PeakWidthAt10PctFailureReasonColumn { get; } = "PeakWidthAt10PctFailureReason";
		internal static string PeakWidthAt50PctAverageColumn { get; } = "PeakWidthAt50PctAverage";
		internal static string PeakWidthAt50PctStDevColumn { get; } = "PeakWidthAt50PctStDev";
		internal static string PeakWidthAt50PctRelativeStdDevPercentColumn { get; } = "PeakWidthAt50PctRelativeStdDevPercent";
		internal static string PeakWidthAt50PctRelativeStdDevPassedColumn { get; } = "PeakWidthAt50PctRelativeStdDevPassed";
		internal static string PeakWidthAt50PctFailureReasonColumn { get; } = "PeakWidthAt50PctFailureReason";
		internal static string SstFlagColumn { get; } = "SstFlag";

		protected readonly string SelectSql =
			$"SELECT {TableName}.{IdColumn}," +
			$"{TableName}.{AnalysisResultSetIdColumn}," +
			$"{TableName}.{CompoundGuidColumn}," +
			$"{TableName}.{AreaAverageColumn}," +
			$"{TableName}.{AreaStDevColumn}," +
			$"{TableName}.{AreaRelativeStdDevPercentColumn}," +
			$"{TableName}.{AreaRelativeStdDevPassedColumn}," +
			$"{TableName}.{AreaFailureReasonColumn}," +
			$"{TableName}.{HeightAverageColumn}," +
			$"{TableName}.{HeightStDevColumn}," +
			$"{TableName}.{HeightRelativeStdDevPercentColumn}," +
			$"{TableName}.{HeightRelativeStdDevPassedColumn}," +
			$"{TableName}.{HeightFailureReasonColumn}," +
			$"{TableName}.{TheoreticalPlatesNAverageColumn}," +
			$"{TableName}.{TheoreticalPlatesNStDevColumn}," +
			$"{TableName}.{TheoreticalPlatesNRelativeStdDevPercentColumn}," +
			$"{TableName}.{TheoreticalPlatesNRelativeStdDevPassedColumn}," +
			$"{TableName}.{TheoreticalPlatesNFailureReasonColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanAverageColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanStDevColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanRelativeStdDevPercentColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanRelativeStdDevPassedColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanFailureReasonColumn}," +
			$"{TableName}.{TailingFactorSymmetryAverageColumn}," +
			$"{TableName}.{TailingFactorSymmetryStDevColumn}," +
			$"{TableName}.{TailingFactorSymmetryRelativeStdDevPercentColumn}," +
			$"{TableName}.{TailingFactorSymmetryRelativeStdDevPassedColumn}," +
			$"{TableName}.{TailingFactorSymmetryFailureReasonColumn}," +
			$"{TableName}.{RelativeRetentionAverageColumn}," +
			$"{TableName}.{RelativeRetentionStDevColumn}," +
			$"{TableName}.{RelativeRetentionRelativeStdDevPercentColumn}," +
			$"{TableName}.{RelativeRetentionRelativeStdDevPassedColumn}," +
			$"{TableName}.{RelativeRetentionFailureReasonColumn}," +
            $"{TableName}.{RelativeRetentionTimeAverageColumn}," +
            $"{TableName}.{RelativeRetentionTimeStDevColumn}," +
            $"{TableName}.{RelativeRetentionTimeRelativeStdDevPercentColumn}," +
            $"{TableName}.{RelativeRetentionTimeRelativeStdDevPassedColumn}," +
            $"{TableName}.{RelativeRetentionTimeFailureReasonColumn}," +
            $"{TableName}.{RetentionTimeAverageColumn}," +
            $"{TableName}.{RetentionTimeStDevColumn}," +
            $"{TableName}.{RetentionTimeRelativeStdDevPercentColumn}," +
            $"{TableName}.{RetentionTimeRelativeStdDevPassedColumn}," +
            $"{TableName}.{RetentionTimeFailureReasonColumn}," +
            $"{TableName}.{CapacityFactorKPrimeAverageColumn}," +
			$"{TableName}.{CapacityFactorKPrimeStDevColumn}," +
			$"{TableName}.{CapacityFactorKPrimeRelativeStdDevPercentColumn}," +
			$"{TableName}.{CapacityFactorKPrimeRelativeStdDevPassedColumn}," +
			$"{TableName}.{CapacityFactorKPrimeFailureReasonColumn}," +
			$"{TableName}.{ResolutionAverageColumn}," +
			$"{TableName}.{ResolutionStDevColumn}," +
			$"{TableName}.{ResolutionRelativeStdDevPercentColumn}," +
			$"{TableName}.{ResolutionRelativeStdDevPassedColumn}," +
			$"{TableName}.{ResolutionFailureReasonumn}," +
			$"{TableName}.{UspResolutionAverageColumn}," +
			$"{TableName}.{UspResolutionStDevColumn}," +
			$"{TableName}.{UspResolutionRelativeStdDevPercentColumn}," +
			$"{TableName}.{UspResolutionRelativeStdDevPassedColumn}," +
			$"{TableName}.{UspResolutionFailureReasonColumn}," +
			$"{TableName}.{SignalToNoiseAverageColumn}," +
			$"{TableName}.{SignalToNoiseStDevColumn}," +
			$"{TableName}.{SignalToNoiseRelativeStdDevPercentColumn}," +
			$"{TableName}.{SignalToNoiseRelativeStdDevPassedColumn}," +
			$"{TableName}.{SignalToNoiseFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAtBaseAverageColumn}," +
			$"{TableName}.{PeakWidthAtBaseStDevColumn}," +
			$"{TableName}.{PeakWidthAtBaseRelativeStdDevPercentColumn}," +
			$"{TableName}.{PeakWidthAtBaseRelativeStdDevPassedColumn}," +
			$"{TableName}.{PeakWidthAtBaseFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAt5PctAverageColumn}," +
			$"{TableName}.{PeakWidthAt5PctStDevColumn}," +
			$"{TableName}.{PeakWidthAt5PctRelativeStdDevPercentColumn}," +
			$"{TableName}.{PeakWidthAt5PctRelativeStdDevPassedColumn}," +
			$"{TableName}.{PeakWidthAt5PctFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAt10PctAverageColumn}," +
			$"{TableName}.{PeakWidthAt10PctStDevColumn}," +
			$"{TableName}.{PeakWidthAt10PctRelativeStdDevPercentColumn}," +
			$"{TableName}.{PeakWidthAt10PctRelativeStdDevPassedColumn}," +
			$"{TableName}.{PeakWidthAt10PctFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAt50PctAverageColumn}," +
			$"{TableName}.{PeakWidthAt50PctStDevColumn}," +
			$"{TableName}.{PeakWidthAt50PctRelativeStdDevPercentColumn}," +
			$"{TableName}.{PeakWidthAt50PctRelativeStdDevPassedColumn}," +
			$"{TableName}.{PeakWidthAt50PctFailureReasonColumn}," +
			$"{TableName}.{SstFlagColumn} ";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} (" +
			$"{AnalysisResultSetIdColumn}," +
			$"{CompoundGuidColumn}," +
			$"{AreaAverageColumn}," +
			$"{AreaStDevColumn}," +
			$"{AreaRelativeStdDevPercentColumn}," +
			$"{AreaRelativeStdDevPassedColumn}," +
			$"{AreaFailureReasonColumn}," +
			$"{HeightAverageColumn}," +
			$"{HeightStDevColumn}," +
			$"{HeightRelativeStdDevPercentColumn}," +
			$"{HeightRelativeStdDevPassedColumn}," +
			$"{HeightFailureReasonColumn}," +
			$"{TheoreticalPlatesNAverageColumn}," +
			$"{TheoreticalPlatesNStDevColumn}," +
			$"{TheoreticalPlatesNRelativeStdDevPercentColumn}," +
			$"{TheoreticalPlatesNRelativeStdDevPassedColumn}," +
			$"{TheoreticalPlatesNFailureReasonColumn}," +
			$"{TheoreticalPlatesNTanAverageColumn}," +
			$"{TheoreticalPlatesNTanStDevColumn}," +
			$"{TheoreticalPlatesNTanRelativeStdDevPercentColumn}," +
			$"{TheoreticalPlatesNTanRelativeStdDevPassedColumn}," +
			$"{TheoreticalPlatesNTanFailureReasonColumn}," +
			$"{TailingFactorSymmetryAverageColumn}," +
			$"{TailingFactorSymmetryStDevColumn}," +
			$"{TailingFactorSymmetryRelativeStdDevPercentColumn}," +
			$"{TailingFactorSymmetryRelativeStdDevPassedColumn}," +
			$"{TailingFactorSymmetryFailureReasonColumn}," +
			$"{RelativeRetentionAverageColumn}," +
			$"{RelativeRetentionStDevColumn}," +
			$"{RelativeRetentionRelativeStdDevPercentColumn}," +
			$"{RelativeRetentionRelativeStdDevPassedColumn}," +
			$"{RelativeRetentionFailureReasonColumn}," +
            $"{RelativeRetentionTimeAverageColumn}," +
            $"{RelativeRetentionTimeStDevColumn}," +
            $"{RelativeRetentionTimeRelativeStdDevPercentColumn}," +
            $"{RelativeRetentionTimeRelativeStdDevPassedColumn}," +
            $"{RelativeRetentionTimeFailureReasonColumn}," +
            $"{RetentionTimeAverageColumn}," +
            $"{RetentionTimeStDevColumn}," +
            $"{RetentionTimeRelativeStdDevPercentColumn}," +
            $"{RetentionTimeRelativeStdDevPassedColumn}," +
            $"{RetentionTimeFailureReasonColumn}," +
            $"{CapacityFactorKPrimeAverageColumn}," +
			$"{CapacityFactorKPrimeStDevColumn}," +
			$"{CapacityFactorKPrimeRelativeStdDevPercentColumn}," +
			$"{CapacityFactorKPrimeRelativeStdDevPassedColumn}," +
			$"{CapacityFactorKPrimeFailureReasonColumn}," +
			$"{ResolutionAverageColumn}," +
			$"{ResolutionStDevColumn}," +
			$"{ResolutionRelativeStdDevPercentColumn}," +
			$"{ResolutionRelativeStdDevPassedColumn}," +
			$"{ResolutionFailureReasonumn}," +
			$"{UspResolutionAverageColumn}," +
			$"{UspResolutionStDevColumn}," +
			$"{UspResolutionRelativeStdDevPercentColumn}," +
			$"{UspResolutionRelativeStdDevPassedColumn}," +
			$"{UspResolutionFailureReasonColumn}," +
			$"{SignalToNoiseAverageColumn}," +
			$"{SignalToNoiseStDevColumn}," +
			$"{SignalToNoiseRelativeStdDevPercentColumn}," +
			$"{SignalToNoiseRelativeStdDevPassedColumn}," +
			$"{SignalToNoiseFailureReasonColumn}," +
			$"{PeakWidthAtBaseAverageColumn}," +
			$"{PeakWidthAtBaseStDevColumn}," +
			$"{PeakWidthAtBaseRelativeStdDevPercentColumn}," +
			$"{PeakWidthAtBaseRelativeStdDevPassedColumn}," +
			$"{PeakWidthAtBaseFailureReasonColumn}," +
			$"{PeakWidthAt5PctAverageColumn}," +
			$"{PeakWidthAt5PctStDevColumn}," +
			$"{PeakWidthAt5PctRelativeStdDevPercentColumn}," +
			$"{PeakWidthAt5PctRelativeStdDevPassedColumn}," +
			$"{PeakWidthAt5PctFailureReasonColumn}," +
			$"{PeakWidthAt10PctAverageColumn}," +
			$"{PeakWidthAt10PctStDevColumn}," +
			$"{PeakWidthAt10PctRelativeStdDevPercentColumn}," +
			$"{PeakWidthAt10PctRelativeStdDevPassedColumn}," +
			$"{PeakWidthAt10PctFailureReasonColumn}," +
			$"{PeakWidthAt50PctAverageColumn}," +
			$"{PeakWidthAt50PctStDevColumn}," +
			$"{PeakWidthAt50PctRelativeStdDevPercentColumn}," +
			$"{PeakWidthAt50PctRelativeStdDevPassedColumn}," +
			$"{PeakWidthAt50PctFailureReasonColumn}," +
			$"{SstFlagColumn}" +
			") " +
			"VALUES (" +
			$"@{AnalysisResultSetIdColumn}," +
			$"@{CompoundGuidColumn}," +
			$"@{AreaAverageColumn}," +
			$"@{AreaStDevColumn}," +
			$"@{AreaRelativeStdDevPercentColumn}," +
			$"@{AreaRelativeStdDevPassedColumn}," +
			$"@{AreaFailureReasonColumn}," +
			$"@{HeightAverageColumn}," +
			$"@{HeightStDevColumn}," +
			$"@{HeightRelativeStdDevPercentColumn}," +
			$"@{HeightRelativeStdDevPassedColumn}," +
			$"@{HeightFailureReasonColumn}," +
			$"@{TheoreticalPlatesNAverageColumn}," +
			$"@{TheoreticalPlatesNStDevColumn}," +
			$"@{TheoreticalPlatesNRelativeStdDevPercentColumn}," +
			$"@{TheoreticalPlatesNRelativeStdDevPassedColumn}," +
			$"@{TheoreticalPlatesNFailureReasonColumn}," +
			$"@{TheoreticalPlatesNTanAverageColumn}," +
			$"@{TheoreticalPlatesNTanStDevColumn}," +
			$"@{TheoreticalPlatesNTanRelativeStdDevPercentColumn}," +
			$"@{TheoreticalPlatesNTanRelativeStdDevPassedColumn}," +
			$"@{TheoreticalPlatesNTanFailureReasonColumn}," +
			$"@{TailingFactorSymmetryAverageColumn}," +
			$"@{TailingFactorSymmetryStDevColumn}," +
			$"@{TailingFactorSymmetryRelativeStdDevPercentColumn}," +
			$"@{TailingFactorSymmetryRelativeStdDevPassedColumn}," +
			$"@{TailingFactorSymmetryFailureReasonColumn}," +
			$"@{RelativeRetentionAverageColumn}," +
			$"@{RelativeRetentionStDevColumn}," +
			$"@{RelativeRetentionRelativeStdDevPercentColumn}," +
			$"@{RelativeRetentionRelativeStdDevPassedColumn}," +
			$"@{RelativeRetentionFailureReasonColumn}," +
            $"@{RelativeRetentionTimeAverageColumn}," +
            $"@{RelativeRetentionTimeStDevColumn}," +
            $"@{RelativeRetentionTimeRelativeStdDevPercentColumn}," +
            $"@{RelativeRetentionTimeRelativeStdDevPassedColumn}," +
            $"@{RelativeRetentionTimeFailureReasonColumn}," +
            $"@{RetentionTimeAverageColumn}," +
            $"@{RetentionTimeStDevColumn}," +
            $"@{RetentionTimeRelativeStdDevPercentColumn}," +
            $"@{RetentionTimeRelativeStdDevPassedColumn}," +
            $"@{RetentionTimeFailureReasonColumn}," +
            $"@{CapacityFactorKPrimeAverageColumn}," +
			$"@{CapacityFactorKPrimeStDevColumn}," +
			$"@{CapacityFactorKPrimeRelativeStdDevPercentColumn}," +
			$"@{CapacityFactorKPrimeRelativeStdDevPassedColumn}," +
			$"@{CapacityFactorKPrimeFailureReasonColumn}," +
			$"@{ResolutionAverageColumn}," +
			$"@{ResolutionStDevColumn}," +
			$"@{ResolutionRelativeStdDevPercentColumn}," +
			$"@{ResolutionRelativeStdDevPassedColumn}," +
			$"@{ResolutionFailureReasonumn}," +
			$"@{UspResolutionAverageColumn}," +
			$"@{UspResolutionStDevColumn}," +
			$"@{UspResolutionRelativeStdDevPercentColumn}," +
			$"@{UspResolutionRelativeStdDevPassedColumn}," +
			$"@{UspResolutionFailureReasonColumn}," +
			$"@{SignalToNoiseAverageColumn}," +
			$"@{SignalToNoiseStDevColumn}," +
			$"@{SignalToNoiseRelativeStdDevPercentColumn}," +
			$"@{SignalToNoiseRelativeStdDevPassedColumn}," +
			$"@{SignalToNoiseFailureReasonColumn}," +
			$"@{PeakWidthAtBaseAverageColumn}," +
			$"@{PeakWidthAtBaseStDevColumn}," +
			$"@{PeakWidthAtBaseRelativeStdDevPercentColumn}," +
			$"@{PeakWidthAtBaseRelativeStdDevPassedColumn}," +
			$"@{PeakWidthAtBaseFailureReasonColumn}," +
			$"@{PeakWidthAt5PctAverageColumn}," +
			$"@{PeakWidthAt5PctStDevColumn}," +
			$"@{PeakWidthAt5PctRelativeStdDevPercentColumn}," +
			$"@{PeakWidthAt5PctRelativeStdDevPassedColumn}," +
			$"@{PeakWidthAt5PctFailureReasonColumn}," +
			$"@{PeakWidthAt10PctAverageColumn}," +
			$"@{PeakWidthAt10PctStDevColumn}," +
			$"@{PeakWidthAt10PctRelativeStdDevPercentColumn}," +
			$"@{PeakWidthAt10PctRelativeStdDevPassedColumn}," +
			$"@{PeakWidthAt10PctFailureReasonColumn}," +
			$"@{PeakWidthAt50PctAverageColumn}," +
			$"@{PeakWidthAt50PctStDevColumn}," +
			$"@{PeakWidthAt50PctRelativeStdDevPercentColumn}," +
			$"@{PeakWidthAt50PctRelativeStdDevPassedColumn}," +
			$"@{PeakWidthAt50PctFailureReasonColumn}," +
			$"@{SstFlagColumn}" +
			") ";

		public IList<CompoundSuitabilitySummaryResults> GetCompoundSuitabilitySummaryResults(
			IDbConnection connection, string projectName, string analysisResultSetName)
		{
			try
			{
				projectName = projectName.ToLower();
				analysisResultSetName = analysisResultSetName.ToLower();

				string sql = $"{SelectSql} " +
				             $"FROM {ProjectDao.TableName} " +
				             $"INNER JOIN {AnalysisResultSetDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} " +
				             $"INNER JOIN {TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} = {TableName}.{AnalysisResultSetIdColumn} " +
				             $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND " +
				             $"LOWER({AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.NameColumn}) = @AnalysisResultSetName";

				return connection.Query<CompoundSuitabilitySummaryResults>(sql, new { ProjectName = projectName, AnalysisResultSetName = analysisResultSetName}).ToList();

			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetCompoundSuitabilitySummaryResults method", ex);
				throw;
			}
		}
		public IList<CompoundSuitabilitySummaryResults> GetCompoundSuitabilitySummaryResults(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
		{
			try
			{
				string sql = $"{SelectSql} " +
				             $"FROM {ProjectDao.TableName} " +
				             $"INNER JOIN {AnalysisResultSetDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} " +
				             $"INNER JOIN {TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} = {TableName}.{AnalysisResultSetIdColumn} " +
				             $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				             $"{AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.GuidColumn} = @AnalysisResultSetGuid";

				return connection.Query<CompoundSuitabilitySummaryResults>(sql,
					new
					{
						ProjectGuid = projectGuid,
						AnalysisResultSetGuid = analysisResultSetGuid
					}).ToList();

			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetCompoundSuitabilitySummaryResults method", ex);
				throw;
			}
		}
		public IList<CompoundSuitabilitySummaryResults> GetCompoundSuitabilitySummaryResults(IDbConnection connection, long analysisResultSetId)
        {
	        try
	        {
		        string sql = $"{SelectSql} " +
		                     $"FROM {TableName} " +
		                     $"WHERE {AnalysisResultSetIdColumn} = @{AnalysisResultSetIdColumn}";

		        return connection.Query<CompoundSuitabilitySummaryResults>(sql, new { AnalysisResultSetId = analysisResultSetId }).ToList();

	        }
	        catch (Exception ex)
	        {
		        Log.Error($"Error in GetCompoundSuitabilitySummaryResults method", ex);
		        throw;
	        }
        }
		public bool Create(IDbConnection connection, IList<CompoundSuitabilitySummaryResults> compoundSuitabilityResults)
		{
			try
			{
				var numberOfRecordsSaved = connection.Execute(InsertSql, compoundSuitabilityResults);
				return (numberOfRecordsSaved != 0);
			}
			catch (Exception ex)
			{
				Log.Error(
					$"Error occurred in Create method", ex);
				throw;
			}
		}

		public bool Delete(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} WHERE {AnalysisResultSetIdColumn} = {analysisResultSetId} ");
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}

	}
}