using System;
using SuitabilityResult15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.SuitabilityResult;
using SuitabilityResult16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.SuitabilityResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class SuitabilityResult
    {
        public static SuitabilityResult16 Transform(SuitabilityResult15 suitabilityResult)
        {
            if (suitabilityResult == null) return null;

            return new SuitabilityResult16
            {
                Id = suitabilityResult.Id,
                CalculatedChannelDataId = suitabilityResult.CalculatedChannelDataId,
                PeakGuid = suitabilityResult.PeakGuid,
                CompoundGuid = suitabilityResult.CompoundGuid,
                PeakName = suitabilityResult.PeakName,
                PeakRetentionTime = suitabilityResult.PeakRetentionTime,
                Area = suitabilityResult.Area,
                AreaPassed = suitabilityResult.AreaPassed,
                AreaFailureReason = suitabilityResult.AreaFailureReason,
                Height = suitabilityResult.Height,
                HeightPassed = suitabilityResult.HeightPassed,
                HeightFailureReason = suitabilityResult.HeightFailureReason,
                TheoreticalPlatesN = suitabilityResult.TheoreticalPlatesN,
                TheoreticalPlatesNPassed = suitabilityResult.TheoreticalPlatesNPassed,
                TheoreticalPlatesNFailureReason = suitabilityResult.TheoreticalPlatesNFailureReason,
                TheoreticalPlatesNTan = suitabilityResult.TheoreticalPlatesNTan,
                TheoreticalPlatesNTanPassed = suitabilityResult.TheoreticalPlatesNTanPassed,
                TheoreticalPlatesNTanFailureReason = suitabilityResult.TheoreticalPlatesNTanFailureReason,
                //TODO : Need decide default value for this property
                TheoreticalPlatesNFoleyDorsey = null,
                TheoreticalPlatesNFoleyDorseyPassed = false,
                TheoreticalPlatesNFoleyDorseyFailureReason = 0,
                TailingFactorSymmetry = suitabilityResult.TailingFactorSymmetry,
                TailingFactorSymmetryPassed = suitabilityResult.TailingFactorSymmetryPassed,
                TailingFactorSymmetryFailureReason = suitabilityResult.TailingFactorSymmetryFailureReason,
                RelativeRetention = suitabilityResult.RelativeRetention,
                RelativeRetentionTime = suitabilityResult.RelativeRetentionTime,
                RelativeRetentionPassed = suitabilityResult.RelativeRetentionPassed,
                RelativeRetentionFailureReason = suitabilityResult.RelativeRetentionFailureReason,
                RelativeRetentionTimePassed = suitabilityResult.RelativeRetentionTimePassed,
                RelativeRetentionTimeFailureReason = suitabilityResult.RelativeRetentionTimeFailureReason,
                RetentionTime = suitabilityResult.RetentionTime,
                RetentionTimePassed = suitabilityResult.RetentionTimePassed,
                RetentionTimeFailureReason = suitabilityResult.RetentionTimeFailureReason,
                CapacityFactorKPrime = suitabilityResult.CapacityFactorKPrime,
                CapacityFactorKPrimePassed = suitabilityResult.CapacityFactorKPrimePassed,
                CapacityFactorKPrimeFailureReason = suitabilityResult.CapacityFactorKPrimeFailureReason,
                Resolution = suitabilityResult.Resolution,
                //TODO : Need decide default value for this property
                ResolutionReferencePeakRetentionTime = null,
                ResolutionReferencePeakGuid = Guid.Empty,
                ResolutionPassed = suitabilityResult.ResolutionPassed,
                ResolutionFailureReason = suitabilityResult.ResolutionFailureReason,
                UspResolution = suitabilityResult.UspResolution,
                UspResolutionPassed = suitabilityResult.UspResolutionPassed,
                UspResolutionFailureReason = suitabilityResult.UspResolutionFailureReason,
                SignalToNoise = suitabilityResult.SignalToNoise,
                SignalToNoisePassed = suitabilityResult.SignalToNoisePassed,
                SignalToNoiseFailureReason = suitabilityResult.SignalToNoiseFailureReason,
                PeakWidthAtBase = suitabilityResult.PeakWidthAtBase,
                PeakWidthAtBasePassed = suitabilityResult.PeakWidthAtBasePassed,
                PeakWidthAtBaseFailureReason = suitabilityResult.PeakWidthAtBaseFailureReason,
                PeakWidthAt5Pct = suitabilityResult.PeakWidthAt5Pct,
                PeakWidthAt5PctPassed = suitabilityResult.PeakWidthAt5PctPassed,
                PeakWidthAt5PctFailureReason = suitabilityResult.PeakWidthAt5PctFailureReason,
                PeakWidthAt10Pct = suitabilityResult.PeakWidthAt10Pct,
                PeakWidthAt10PctPassed = suitabilityResult.PeakWidthAt10PctPassed,
                PeakWidthAt10PctFailureReason = suitabilityResult.PeakWidthAt10PctFailureReason,
                PeakWidthAt50Pct = suitabilityResult.PeakWidthAt50Pct,
                PeakWidthAt50PctPassed = suitabilityResult.PeakWidthAt50PctPassed,
                PeakWidthAt50PctFailureReason = suitabilityResult.PeakWidthAt50PctFailureReason,
                Noise = suitabilityResult.Noise,
                NoiseStart = suitabilityResult.NoiseStart,
                NoiseGapStart = suitabilityResult.NoiseGapStart,
                NoiseGapEnd = suitabilityResult.NoiseGapEnd,
                NoiseEnd = suitabilityResult.NoiseEnd,
                SstFlag = suitabilityResult.SstFlag
            };
        }
    }
}
