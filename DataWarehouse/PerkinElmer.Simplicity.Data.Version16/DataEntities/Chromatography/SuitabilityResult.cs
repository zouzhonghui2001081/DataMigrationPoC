
using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class SuitabilityResult
    {
        public long Id { get; set; }
        public long CalculatedChannelDataId { get; set; }
        public Guid PeakGuid { get; set; }
        public Guid CompoundGuid { get; set; }
        public string PeakName { get; set; }
        public double PeakRetentionTime { get; set; }
        public double? Area { get; set; }
        public bool AreaPassed { get; set; }
        public short AreaFailureReason { get; set; }
        public double? Height { get; set; }
        public bool HeightPassed { get; set; }
        public short HeightFailureReason { get; set; }
        public double? TheoreticalPlatesN { get; set; }
        public bool TheoreticalPlatesNPassed { get; set; }
        public short TheoreticalPlatesNFailureReason { get; set; }
        public double? TheoreticalPlatesNTan { get; set; }
        public bool TheoreticalPlatesNTanPassed { get; set; }
        public short TheoreticalPlatesNTanFailureReason { get; set; }
        public double? TheoreticalPlatesNFoleyDorsey { get; set; }
        public bool TheoreticalPlatesNFoleyDorseyPassed { get; set; }
        public short TheoreticalPlatesNFoleyDorseyFailureReason { get; set; }
        public double? TailingFactorSymmetry { get; set; }
        public bool TailingFactorSymmetryPassed { get; set; }
        public short TailingFactorSymmetryFailureReason { get; set; }
        public double? RelativeRetention { get; set; }
        public double? RelativeRetentionTime { get; set; }
        public bool RelativeRetentionPassed { get; set; }
        public short RelativeRetentionFailureReason { get; set; }
        public bool RelativeRetentionTimePassed { get; set; }
        public short RelativeRetentionTimeFailureReason { get; set; }
        public double? RetentionTime { get; set; }
        public bool RetentionTimePassed { get; set; }
        public short RetentionTimeFailureReason { get; set; }
        public double? CapacityFactorKPrime { get; set; }
        public bool CapacityFactorKPrimePassed { get; set; }
        public short CapacityFactorKPrimeFailureReason { get; set; }
        public double? Resolution { get; set; }
        public double? ResolutionReferencePeakRetentionTime { get; set; }
        public Guid ResolutionReferencePeakGuid { get; set; }
        public bool ResolutionPassed { get; set; }
        public short ResolutionFailureReason { get; set; }
        public double? UspResolution { get; set; }
        public bool UspResolutionPassed { get; set; }
        public short UspResolutionFailureReason { get; set; }
        public double? SignalToNoise { get; set; }
        public bool SignalToNoisePassed { get; set; }
        public short SignalToNoiseFailureReason { get; set; }
        public double? PeakWidthAtBase { get; set; }
        public bool PeakWidthAtBasePassed { get; set; }
        public short PeakWidthAtBaseFailureReason { get; set; }
        public double? PeakWidthAt5Pct { get; set; }
        public bool PeakWidthAt5PctPassed { get; set; }
        public short PeakWidthAt5PctFailureReason { get; set; }
        public double? PeakWidthAt10Pct { get; set; }
        public bool PeakWidthAt10PctPassed { get; set; }
        public short PeakWidthAt10PctFailureReason { get; set; }
        public double? PeakWidthAt50Pct { get; set; }
        public bool PeakWidthAt50PctPassed { get; set; }
        public short PeakWidthAt50PctFailureReason { get; set; }
        public double? Noise { get; set; }
        public double? NoiseStart { get; set; }
        public double? NoiseGapStart { get; set; }
        public double? NoiseGapEnd { get; set; }
        public double? NoiseEnd { get; set; }
        public bool? SstFlag { get; set; }
    }
}
