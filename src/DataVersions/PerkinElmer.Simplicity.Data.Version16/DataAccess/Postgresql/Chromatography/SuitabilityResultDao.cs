using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class SuitabilityResultDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "SuitabilityResult";
		internal static string IdColumn { get; } = "Id";
		internal static string CalculatedChannelDataIdColumn { get; } = "CalculatedChannelDataId";
		internal static string PeakGuidColumn { get; } = "PeakGuid";
		internal static string CompoundGuidColumn { get; } = "CompoundGuid";
		internal static string PeakNameColumn { get; } = "PeakName";
		internal static string PeakRetentionTimeColumn { get; } = "PeakRetentionTime";
		internal static string AreaColumn { get; } = "Area";
		internal static string AreaPassedColumn { get; } = "AreaPassed";
		internal static string AreaFailureReasonColumn { get; } = "AreaFailureReason";
		internal static string HeightColumn { get; } = "Height";
		internal static string HeightPassedColumn { get; } = "HeightPassed";
		internal static string HeightFailureReasonColumn { get; } = "HeightFailureReason";
		internal static string TheoreticalPlatesNColumn { get; } = "TheoreticalPlatesN";
		internal static string TheoreticalPlatesNPassedColumn { get; } = "TheoreticalPlatesNPassed";
		internal static string TheoreticalPlatesNFailureReasonColumn { get; } = "TheoreticalPlatesNFailureReason";
		internal static string TheoreticalPlatesNTanColumn { get; } = "TheoreticalPlatesNTan";
		internal static string TheoreticalPlatesNTanPassedColumn { get; } = "TheoreticalPlatesNTanPassed";
		internal static string TheoreticalPlatesNTanFailureReasonColumn { get; } = "TheoreticalPlatesNTanFailureReason";

        internal static string TheoreticalPlatesNFoleyDorseyColumn { get; } = "TheoreticalPlatesNFoleyDorsey";
        internal static string TheoreticalPlatesNFoleyDorseyPassedColumn { get; } = "TheoreticalPlatesNFoleyDorseyPassed";
        internal static string TheoreticalPlatesNFoleyDorseyFailureReasonColumn { get; } = "TheoreticalPlatesNFoleyDorseyFailureReason";


        internal static string TailingFactorSymmetryColumn { get; } = "TailingFactorSymmetry";
		internal static string TailingFactorSymmetryPassedColumn { get; } = "TailingFactorSymmetryPassed";
		internal static string TailingFactorSymmetryFailureReasonColumn { get; } = "TailingFactorSymmetryFailureReason";
		internal static string RelativeRetentionColumn { get; } = "RelativeRetention";
		internal static string RelativeRetentionTimeColumn { get; } = "RelativeRetentionTime";
        internal static string RelativeRetentionPassedColumn { get; } = "RelativeRetentionPassed";
		internal static string RelativeRetentionFailureReasonColumn { get; } = "RelativeRetentionFailureReason";
        internal static string RelativeRetentionTimePassedColumn { get; } = "RelativeRetentionTimePassed";
        internal static string RelativeRetentionTimeFailureReasonColumn { get; } = "RelativeRetentionTimeFailureReason";

        internal static string RetentionTimeColumn { get; } = "RetentionTime";
        internal static string RetentionTimePassedColumn { get; } = "RetentionTimePassed";
        internal static string RetentionTimeFailureReasonColumn { get; } = "RetentionTimeFailureReason";

        internal static string CapacityFactorKPrimeColumn { get; } = "CapacityFactorKPrime";
		internal static string CapacityFactorKPrimePassedColumn { get; } = "CapacityFactorKPrimePassed";
		internal static string CapacityFactorKPrimeFailureReasonColumn { get; } = "CapacityFactorKPrimeFailureReason";
		internal static string ResolutionReferencePeakRetentionTimeColumn { get; } = "ResolutionReferencePeakRetentionTime";
		internal static string ResolutionReferencePeakGuidColumn { get; } = "ResolutionReferencePeakGuid";
        
        internal static string ResolutionColumn { get; } = "Resolution";
        internal static string ResolutionPassedColumn { get; } = "ResolutionPassed";
		internal static string ResolutionFailureReasonColumn { get; } = "ResolutionFailureReason";
		internal static string UspResolutionColumn { get; } = "UspResolution";
		internal static string UspResolutionPassedColumn { get; } = "UspResolutionPassed";
		internal static string UspResolutionFailureReasonColumn { get; } = "UspResolutionFailureReason";
		internal static string SignalToNoiseColumn { get; } = "SignalToNoise";
		internal static string SignalToNoisePassedColumn { get; } = "SignalToNoisePassed";
		internal static string SignalToNoiseFailureReasonColumn { get; } = "SignalToNoiseFailureReason";
		internal static string PeakWidthAtBaseColumn { get; } = "PeakWidthAtBase";
		internal static string PeakWidthAtBasePassedColumn { get; } = "PeakWidthAtBasePassed";
		internal static string PeakWidthAtBaseFailureReasonColumn { get; } = "PeakWidthAtBaseFailureReason";
		internal static string PeakWidthAt5PctColumn { get; } = "PeakWidthAt5Pct";
		internal static string PeakWidthAt5PctPassedColumn { get; } = "PeakWidthAt5PctPassed";
		internal static string PeakWidthAt5PctFailureReasonColumn { get; } = "PeakWidthAt5PctFailureReason";
		internal static string PeakWidthAt10PctColumn { get; } = "PeakWidthAt10Pct";
		internal static string PeakWidthAt10PctPassedColumn { get; } = "PeakWidthAt10PctPassed";
		internal static string PeakWidthAt10PctFailureReasonColumn { get; } = "PeakWidthAt10PctFailureReason";
		internal static string PeakWidthAt50PctColumn { get; } = "PeakWidthAt50Pct";
		internal static string PeakWidthAt50PctPassedColumn { get; } = "PeakWidthAt50PctPassed";
		internal static string PeakWidthAt50PctFailureReasonColumn { get; } = "PeakWidthAt50PctFailureReason";
		internal static string NoiseColumn { get; } = "Noise";
		internal static string NoiseStartColumn { get; } = "NoiseStart";
		internal static string NoiseGapStartColumn { get; } = "NoiseGapStart";
		internal static string NoiseGapEndColumn { get; } = "NoiseGapEnd";
		internal static string NoiseEndColumn { get; } = "NoiseEnd";
		internal static string SstFlagColumn { get; } = "SstFlag";

		protected readonly string SelectSql =
			$"SELECT {TableName}.{IdColumn}," +
			$"{TableName}.{CalculatedChannelDataIdColumn}," +
			$"{TableName}.{PeakGuidColumn}," +
			$"{TableName}.{CompoundGuidColumn}," +
			$"{TableName}.{PeakNameColumn}," +
			$"{TableName}.{PeakRetentionTimeColumn}," +
			$"{TableName}.{AreaColumn}," +
			$"{TableName}.{AreaPassedColumn}," +
			$"{TableName}.{AreaFailureReasonColumn}," +
			$"{TableName}.{HeightColumn}," +
			$"{TableName}.{HeightPassedColumn}," +
			$"{TableName}.{HeightFailureReasonColumn}," +
			$"{TableName}.{TheoreticalPlatesNColumn}," +
			$"{TableName}.{TheoreticalPlatesNPassedColumn}," +
			$"{TableName}.{TheoreticalPlatesNFailureReasonColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanPassedColumn}," +
			$"{TableName}.{TheoreticalPlatesNTanFailureReasonColumn}," +
            $"{TableName}.{TheoreticalPlatesNFoleyDorseyColumn}," +
            $"{TableName}.{TheoreticalPlatesNFoleyDorseyPassedColumn}," +
            $"{TableName}.{TheoreticalPlatesNFoleyDorseyFailureReasonColumn}," +
            $"{TableName}.{TailingFactorSymmetryColumn}," +
			$"{TableName}.{TailingFactorSymmetryPassedColumn}," +
			$"{TableName}.{TailingFactorSymmetryFailureReasonColumn}," +
			$"{TableName}.{RelativeRetentionColumn}," +
			$"{TableName}.{RelativeRetentionTimeColumn}," +
			$"{TableName}.{RetentionTimeColumn}," +
            $"{TableName}.{RetentionTimePassedColumn}," +
            $"{TableName}.{RetentionTimeFailureReasonColumn}," +
            $"{TableName}.{RelativeRetentionPassedColumn}," +
            $"{TableName}.{RelativeRetentionFailureReasonColumn}," +
			$"{TableName}.{RelativeRetentionTimePassedColumn}," +
			$"{TableName}.{RelativeRetentionTimeFailureReasonColumn}," +
            $"{TableName}.{CapacityFactorKPrimeColumn}," +
			$"{TableName}.{CapacityFactorKPrimePassedColumn}," +
			$"{TableName}.{CapacityFactorKPrimeFailureReasonColumn}," +
			$"{TableName}.{ResolutionReferencePeakRetentionTimeColumn}," +
			$"{TableName}.{ResolutionReferencePeakGuidColumn}," +
            $"{TableName}.{ResolutionColumn}," +
			$"{TableName}.{ResolutionPassedColumn}," +
			$"{TableName}.{ResolutionFailureReasonColumn}," +
			$"{TableName}.{UspResolutionColumn}," +
			$"{TableName}.{UspResolutionPassedColumn}," +
			$"{TableName}.{UspResolutionFailureReasonColumn}," +
			$"{TableName}.{SignalToNoiseColumn}," +
			$"{TableName}.{SignalToNoisePassedColumn}," +
			$"{TableName}.{SignalToNoiseFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAtBaseColumn}," +
			$"{TableName}.{PeakWidthAtBasePassedColumn}," +
			$"{TableName}.{PeakWidthAtBaseFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAt5PctColumn}," +
			$"{TableName}.{PeakWidthAt5PctPassedColumn}," +
			$"{TableName}.{PeakWidthAt5PctFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAt10PctColumn}," +
			$"{TableName}.{PeakWidthAt10PctPassedColumn}," +
			$"{TableName}.{PeakWidthAt10PctFailureReasonColumn}," +
			$"{TableName}.{PeakWidthAt50PctColumn}," +
			$"{TableName}.{PeakWidthAt50PctPassedColumn}," +
			$"{TableName}.{PeakWidthAt50PctFailureReasonColumn}," +
			$"{TableName}.{NoiseColumn}," +
			$"{TableName}.{NoiseStartColumn}," +
			$"{TableName}.{NoiseGapStartColumn}," +
			$"{TableName}.{NoiseGapEndColumn}," +
			$"{TableName}.{NoiseEndColumn}," +
			$"{TableName}.{SstFlagColumn} ";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} (" +
			$"{CalculatedChannelDataIdColumn}," +
			$"{PeakGuidColumn}," +
			$"{CompoundGuidColumn}," +
			$"{PeakNameColumn}," +
			$"{PeakRetentionTimeColumn}," +
			$"{AreaColumn}," +
			$"{AreaPassedColumn}," +
			$"{AreaFailureReasonColumn}," +
			$"{HeightColumn}," +
			$"{HeightPassedColumn}," +
			$"{HeightFailureReasonColumn}," +
			$"{TheoreticalPlatesNColumn}," +
			$"{TheoreticalPlatesNPassedColumn}," +
			$"{TheoreticalPlatesNFailureReasonColumn}," +
			$"{TheoreticalPlatesNTanColumn}," +
			$"{TheoreticalPlatesNTanPassedColumn}," +
			$"{TheoreticalPlatesNTanFailureReasonColumn}," +
            $"{TheoreticalPlatesNFoleyDorseyColumn}," +
            $"{TheoreticalPlatesNFoleyDorseyPassedColumn}," +
            $"{TheoreticalPlatesNFoleyDorseyFailureReasonColumn}," +
            $"{TailingFactorSymmetryColumn}," +
			$"{TailingFactorSymmetryPassedColumn}," +
			$"{TailingFactorSymmetryFailureReasonColumn}," +
			$"{RelativeRetentionColumn}," +
			$"{RelativeRetentionTimeColumn}," +
            $"{RelativeRetentionPassedColumn}," +
            $"{RelativeRetentionFailureReasonColumn}," +
            $"{RelativeRetentionTimePassedColumn}," +
            $"{RelativeRetentionTimeFailureReasonColumn}," +
            $"{RetentionTimeColumn}," +
            $"{RetentionTimePassedColumn}," +
            $"{RetentionTimeFailureReasonColumn}," +
            $"{CapacityFactorKPrimeColumn}," +
			$"{CapacityFactorKPrimePassedColumn}," +
			$"{CapacityFactorKPrimeFailureReasonColumn}," +
			$"{ResolutionReferencePeakRetentionTimeColumn}," +
			$"{ResolutionReferencePeakGuidColumn}," +
            $"{ResolutionColumn}," +
            $"{ResolutionPassedColumn}," +
			$"{ResolutionFailureReasonColumn}," +
			$"{UspResolutionColumn}," +
			$"{UspResolutionPassedColumn}," +
			$"{UspResolutionFailureReasonColumn}," +
			$"{SignalToNoiseColumn}," +
			$"{SignalToNoisePassedColumn}," +
			$"{SignalToNoiseFailureReasonColumn}," +
			$"{PeakWidthAtBaseColumn}," +
			$"{PeakWidthAtBasePassedColumn}," +
			$"{PeakWidthAtBaseFailureReasonColumn}," +
			$"{PeakWidthAt5PctColumn}," +
			$"{PeakWidthAt5PctPassedColumn}," +
			$"{PeakWidthAt5PctFailureReasonColumn}," +
			$"{PeakWidthAt10PctColumn}," +
			$"{PeakWidthAt10PctPassedColumn}," +
			$"{PeakWidthAt10PctFailureReasonColumn}," +
			$"{PeakWidthAt50PctColumn}," +
			$"{PeakWidthAt50PctPassedColumn}," +
			$"{PeakWidthAt50PctFailureReasonColumn}," +
			$"{NoiseColumn}," +
			$"{NoiseStartColumn}," +
			$"{NoiseGapStartColumn}," +
			$"{NoiseGapEndColumn}," +
			$"{NoiseEndColumn}," +
			$"{SstFlagColumn}" +
			") " +
			"VALUES (" +
			$"@{CalculatedChannelDataIdColumn}," +
			$"@{PeakGuidColumn}," +
			$"@{CompoundGuidColumn}," +
			$"@{PeakNameColumn}," +
			$"@{PeakRetentionTimeColumn}," +
			$"@{AreaColumn}," +
			$"@{AreaPassedColumn}," +
			$"@{AreaFailureReasonColumn}," +
			$"@{HeightColumn}," +
			$"@{HeightPassedColumn}," +
			$"@{HeightFailureReasonColumn}," +
			$"@{TheoreticalPlatesNColumn}," +
			$"@{TheoreticalPlatesNPassedColumn}," +
			$"@{TheoreticalPlatesNFailureReasonColumn}," +
			$"@{TheoreticalPlatesNTanColumn}," +
			$"@{TheoreticalPlatesNTanPassedColumn}," +
			$"@{TheoreticalPlatesNTanFailureReasonColumn}," +
            $"@{TheoreticalPlatesNFoleyDorseyColumn}," +
            $"@{TheoreticalPlatesNFoleyDorseyPassedColumn}," +
            $"@{TheoreticalPlatesNFoleyDorseyFailureReasonColumn}," +
            $"@{TailingFactorSymmetryColumn}," +
			$"@{TailingFactorSymmetryPassedColumn}," +
			$"@{TailingFactorSymmetryFailureReasonColumn}," +
			$"@{RelativeRetentionColumn}," +
			$"@{RelativeRetentionTimeColumn}," +
            $"@{RelativeRetentionPassedColumn}," +
			$"@{RelativeRetentionFailureReasonColumn}," +
			$"@{RelativeRetentionTimePassedColumn}," +
			$"@{RelativeRetentionTimeFailureReasonColumn}," +
			$"@{RetentionTimeColumn}," +
			$"@{RetentionTimePassedColumn}," +
			$"@{RetentionTimeFailureReasonColumn}," +
            $"@{CapacityFactorKPrimeColumn}," +
			$"@{CapacityFactorKPrimePassedColumn}," +
			$"@{CapacityFactorKPrimeFailureReasonColumn}," +
			$"@{ResolutionReferencePeakRetentionTimeColumn}," +
			$"@{ResolutionReferencePeakGuidColumn}," +
            $"@{ResolutionColumn}," +
            $"@{ResolutionPassedColumn}," +
			$"@{ResolutionFailureReasonColumn}," +
			$"@{UspResolutionColumn}," +
			$"@{UspResolutionPassedColumn}," +
			$"@{UspResolutionFailureReasonColumn}," +
			$"@{SignalToNoiseColumn}," +
			$"@{SignalToNoisePassedColumn}," +
			$"@{SignalToNoiseFailureReasonColumn}," +
			$"@{PeakWidthAtBaseColumn}," +
			$"@{PeakWidthAtBasePassedColumn}," +
			$"@{PeakWidthAtBaseFailureReasonColumn}," +
			$"@{PeakWidthAt5PctColumn}," +
			$"@{PeakWidthAt5PctPassedColumn}," +
			$"@{PeakWidthAt5PctFailureReasonColumn}," +
			$"@{PeakWidthAt10PctColumn}," +
			$"@{PeakWidthAt10PctPassedColumn}," +
			$"@{PeakWidthAt10PctFailureReasonColumn}," +
			$"@{PeakWidthAt50PctColumn}," +
			$"@{PeakWidthAt50PctPassedColumn}," +
			$"@{PeakWidthAt50PctFailureReasonColumn}," +
			$"@{NoiseColumn}," +
			$"@{NoiseStartColumn}," +
			$"@{NoiseGapStartColumn}," +
			$"@{NoiseGapEndColumn}," +
			$"@{NoiseEndColumn}," +
			$"@{SstFlagColumn}" +
			") ";

		public List<SuitabilityResult> GetSuitabilityResultByAnalysisResultSetId(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				string sql = $"{SelectSql} " +
				             $"FROM {BatchRunAnalysisResultDao.TableName} " +
				             $"INNER JOIN {CalculatedChannelDataDao.TableName} ON {BatchRunAnalysisResultDao.TableName}.{BatchRunAnalysisResultDao.IdColumn} = {CalculatedChannelDataDao.TableName}.{CalculatedChannelDataDao.BatchRunAnalysisResultIdColumn} " +
				             $"INNER JOIN {TableName} ON {CalculatedChannelDataDao.TableName}.{CalculatedChannelDataDao.IdColumn} = {TableName}.{CalculatedChannelDataIdColumn} " +
				             $"WHERE {BatchRunAnalysisResultDao.TableName}.{BatchRunAnalysisResultDao.AnalysisResultSetIdColumn} = {analysisResultSetId}";
				
				return connection.Query<SuitabilityResult>(sql).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetSuitabilityResultByAnalysisResultSetId method", ex);
				throw;
			}
		}

		public List<SuitabilityResult> GetSuitabilityResult(IDbConnection connection, long calculatedChannelDataId)
		{
			try
			{
				string sql = $"{SelectSql} " +
				             $"FROM {TableName} " +
				             $"WHERE {TableName}.{CalculatedChannelDataIdColumn} = @CalculatedChannelDataId";

				return connection.Query<SuitabilityResult>(sql, new { CalculatedChannelDataId = calculatedChannelDataId}).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetSuitabilityResultByAnalysisResultSetId method", ex);
				throw;
			}
		}

		public bool Create(IDbConnection connection, SuitabilityResult suitabilityResult)
		{
			try
			{
				var numberOfRecordsSaved = connection.Execute(InsertSql, suitabilityResult);
				return (numberOfRecordsSaved != 0);
			}
			catch (Exception exception)
			{
				Log.Error(
					$"Error occured in Create() method of class{GetType().Name} - {exception.Message}");
				throw;
			}
		}
		public bool Create(IDbConnection connection, IList<SuitabilityResult> suitabilityResults)
		{
			try
			{
				var numberOfRecordsSaved = connection.Execute(InsertSql, suitabilityResults);
				return (numberOfRecordsSaved != 0);
			}
			catch (Exception exception)
			{
				Log.Error(
					$"Error occured in Create() method of class{GetType().Name} - {exception.Message}");
				throw;
			}
		}

		public bool Delete(IDbConnection connection, long calculatedChannelDataId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} WHERE {CalculatedChannelDataIdColumn} = {calculatedChannelDataId} ");
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
