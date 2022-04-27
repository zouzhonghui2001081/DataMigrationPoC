
using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class CompoundSuitabilitySummaryResults
	{
		public long Id { get; set; }
		public long AnalysisResultSetId { get; set; }
		public Guid CompoundGuid { get; set; }
		public double? AreaAverage { get; set; }
		public double? AreaStDev { get; set; }
		public double? AreaRelativeStdDevPercent { get; set; }
		public bool? AreaRelativeStdDevPassed { get; set; }
		public short AreaFailureReason { get; set; }
		public double? HeightAverage { get; set; }
		public double? HeightStDev { get; set; }
		public double? HeightRelativeStdDevPercent { get; set; }
		public bool? HeightRelativeStdDevPassed { get; set; }
		public short HeightFailureReason { get; set; }
		public double? TheoreticalPlatesNAverage { get; set; }
		public double? TheoreticalPlatesNStDev { get; set; }
		public double? TheoreticalPlatesNRelativeStdDevPercent { get; set; }
		public bool? TheoreticalPlatesNRelativeStdDevPassed { get; set; }
		public short TheoreticalPlatesNFailureReason { get; set; }
		public double? TheoreticalPlatesNTanAverage { get; set; }
		public double? TheoreticalPlatesNTanStDev { get; set; }
		public double? TheoreticalPlatesNTanRelativeStdDevPercent { get; set; }
		public bool? TheoreticalPlatesNTanRelativeStdDevPassed { get; set; }
		public short TheoreticalPlatesNTanFailureReason { get; set; }
        public double? TheoreticalPlatesNFoleyDorseyAverage { get; set; }
        public double? TheoreticalPlatesNFoleyDorseyStDev { get; set; }
        public double? TheoreticalPlatesNFoleyDorseyRelativeStdDevPercent { get; set; }
        public bool? TheoreticalPlatesNFoleyDorseyRelativeStdDevPassed { get; set; }
        public short TheoreticalPlatesNFoleyDorseyFailureReason { get; set; }
        public double? TailingFactorSymmetryAverage { get; set; }
		public double? TailingFactorSymmetryStDev { get; set; }
		public double? TailingFactorSymmetryRelativeStdDevPercent { get; set; }
		public bool? TailingFactorSymmetryRelativeStdDevPassed { get; set; }
		public short TailingFactorSymmetryFailureReason { get; set; }
		public double? RelativeRetentionAverage { get; set; }
		public double? RelativeRetentionStDev { get; set; }
		public double? RelativeRetentionRelativeStdDevPercent { get; set; }
		public bool? RelativeRetentionRelativeStdDevPassed { get; set; }
		public short RelativeRetentionFailureReason { get; set; }

        public double? RelativeRetentionTimeAverage { get; set; }
        public double? RelativeRetentionTimeStDev { get; set; }
        public double? RelativeRetentionTimeRelativeStdDevPercent { get; set; }
        public bool? RelativeRetentionTimeRelativeStdDevPassed { get; set; }
        public short RelativeRetentionTimeFailureReason { get; set; }

        public double? RetentionTimeAverage { get; set; }
        public double? RetentionTimeStDev { get; set; }
        public double? RetentionTimeRelativeStdDevPercent { get; set; }
        public bool? RetentionTimeRelativeStdDevPassed { get; set; }
        public short RetentionTimeFailureReason { get; set; }

        public double? CapacityFactorKPrimeAverage { get; set; }
		public double? CapacityFactorKPrimeStDev { get; set; }
		public double? CapacityFactorKPrimeRelativeStdDevPercent { get; set; }
		public bool? CapacityFactorKPrimeRelativeStdDevPassed { get; set; }
		public short CapacityFactorKPrimeFailureReason { get; set; }
		public double? ResolutionAverage { get; set; }
		public double? ResolutionStDev { get; set; }
		public double? ResolutionRelativeStdDevPercent { get; set; }
		public bool? ResolutionRelativeStdDevPassed { get; set; }
		public short ResolutionFailureReason { get; set; }
		public double? UspResolutionAverage { get; set; }
		public double? UspResolutionStDev { get; set; }
		public double? UspResolutionRelativeStdDevPercent { get; set; }
		public bool? UspResolutionRelativeStdDevPassed { get; set; }
		public short UspResolutionFailureReason { get; set; }
		public double? SignalToNoiseAverage { get; set; }
		public double? SignalToNoiseStDev { get; set; }
		public double? SignalToNoiseRelativeStdDevPercent { get; set; }
		public bool? SignalToNoiseRelativeStdDevPassed { get; set; }
		public short SignalToNoiseFailureReason { get; set; }
		public double? PeakWidthAtBaseAverage { get; set; }
		public double? PeakWidthAtBaseStDev { get; set; }
		public double? PeakWidthAtBaseRelativeStdDevPercent { get; set; }
		public bool? PeakWidthAtBaseRelativeStdDevPassed { get; set; }
		public short PeakWidthAtBaseFailureReason { get; set; }
		public double? PeakWidthAt5PctAverage { get; set; }
		public double? PeakWidthAt5PctStDev { get; set; }
		public double? PeakWidthAt5PctRelativeStdDevPercent { get; set; }
		public bool? PeakWidthAt5PctRelativeStdDevPassed { get; set; }
		public short PeakWidthAt5PctFailureReason { get; set; }
		public double? PeakWidthAt10PctAverage { get; set; }
		public double? PeakWidthAt10PctStDev { get; set; }
		public double? PeakWidthAt10PctRelativeStdDevPercent { get; set; }
		public bool? PeakWidthAt10PctRelativeStdDevPassed { get; set; }
		public short PeakWidthAt10PctFailureReason { get; set; }
		public double? PeakWidthAt50PctAverage { get; set; }
		public double? PeakWidthAt50PctStDev { get; set; }
		public double? PeakWidthAt50PctRelativeStdDevPercent { get; set; }
		public bool? PeakWidthAt50PctRelativeStdDevPassed { get; set; }
		public short PeakWidthAt50PctFailureReason { get; set; }
		public bool? SstFlag { get; set; }
	}
}
