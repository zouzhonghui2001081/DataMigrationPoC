using SuitabilityParameters15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.SuitabilityParameters;
using SuitabilityParameters16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.SuitabilityParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class SuitabilityParameters
    {
        public static SuitabilityParameters16 Transform(
            SuitabilityParameters15 suitabilityParameters)
        {
            if (suitabilityParameters == null) return null;
            var suitabilityParameters16 = new SuitabilityParameters16
            {
                Id = suitabilityParameters.Id,
                ChannelMethodId = suitabilityParameters.ChannelMethodId,
                ComplianceStandard = suitabilityParameters.ComplianceStandard,
                EfficiencyReporting = suitabilityParameters.EfficiencyReporting,
                ColumnLength = suitabilityParameters.ColumnLength,
                SignalToNoiseStartTime = suitabilityParameters.SignalToNoiseStartTime,
                SignalToNoiseEndTime = suitabilityParameters.SignalToNoiseEndTime,
                NumberOfSigmas = suitabilityParameters.NumberOfSigmas,
                AnalyzeMode = suitabilityParameters.AnalyzeMode,
                TailingFactorCalculation = suitabilityParameters.TailingFactorCalculation,
                AreaLimitIsUsed = suitabilityParameters.AreaLimitIsUsed,
                AreaLimitLowerLimit = suitabilityParameters.AreaLimitLowerLimit,
                AreaLimitUpperLimit = suitabilityParameters.AreaLimitUpperLimit,
                AreaLimitRelativeStDevPercent = suitabilityParameters.AreaLimitRelativeStDevPercent,
                HeightLimitIsUsed = suitabilityParameters.HeightLimitIsUsed,
                HeightLimitLowerLimit = suitabilityParameters.HeightLimitLowerLimit,
                HeightLimitUpperLimit = suitabilityParameters.HeightLimitUpperLimit,
                HeightLimitRelativeStDevPercent = suitabilityParameters.HeightLimitRelativeStDevPercent,
                NTanLimitIsUsed = suitabilityParameters.NTanLimitIsUsed,
                NTanLimitLowerLimit = suitabilityParameters.NTanLimitLowerLimit,
                NTanLimitUpperLimit = suitabilityParameters.NTanLimitUpperLimit,
                NTanLimitRelativeStDevPercent = suitabilityParameters.NTanLimitRelativeStDevPercent,
                NFoleyLimitIsUsed = suitabilityParameters.NFoleyLimitIsUsed,
                NFoleyLimitLowerLimit = suitabilityParameters.NFoleyLimitLowerLimit,
                NFoleyLimitUpperLimit = suitabilityParameters.NFoleyLimitUpperLimit,
                NFoleyLimitRelativeStDevPercent = suitabilityParameters.NFoleyLimitRelativeStDevPercent,
                //TODO: Foley Dorsey Limit parameter not exist in version 15, need check default parameter
                NFoleyDorseyLimitIsUsed = false,
                NFoleyDorseyLimitLowerLimit = 0.0D,
                NFoleyDorseyLimitUpperLimit = 0.0D,
                NFoleyDorseyLimitRelativeStDevPercent = 0.0D,

                TailingFactorSymmetryLimitIsUsed = suitabilityParameters.TailingFactorSymmetryLimitIsUsed,
                TailingFactorSymmetryLimitLowerLimit = suitabilityParameters.TailingFactorSymmetryLimitLowerLimit,
                TailingFactorSymmetryLimitUpperLimit = suitabilityParameters.TailingFactorSymmetryLimitUpperLimit,
                TailingFactorSymmetryLimitRelativeStDevPercent =
                    suitabilityParameters.TailingFactorSymmetryLimitRelativeStDevPercent,
                UspResolutionLimitIsUsed = suitabilityParameters.UspResolutionLimitIsUsed,
                UspResolutionLimitLowerLimit = suitabilityParameters.UspResolutionLimitLowerLimit,
                UspResolutionLimitUpperLimit = suitabilityParameters.UspResolutionLimitUpperLimit,
                UspResolutionLimitRelativeStDevPercent = suitabilityParameters.UspResolutionLimitRelativeStDevPercent,
                KPrimeLimitIsUsed = suitabilityParameters.KPrimeLimitIsUsed,
                KPrimeLimitLowerLimit = suitabilityParameters.KPrimeLimitLowerLimit,
                KPrimeLimitUpperLimit = suitabilityParameters.KPrimeLimitUpperLimit,
                KPrimeLimitRelativeStDevPercent = suitabilityParameters.KPrimeLimitRelativeStDevPercent,
                ResolutionLimitIsUsed = suitabilityParameters.ResolutionLimitIsUsed,
                ResolutionLimitLowerLimit = suitabilityParameters.ResolutionLimitLowerLimit,
                ResolutionLimitUpperLimit = suitabilityParameters.ResolutionLimitUpperLimit,
                ResolutionLimitRelativeStDevPercent = suitabilityParameters.ResolutionLimitRelativeStDevPercent,
                AlphaLimitIsUsed = suitabilityParameters.AlphaLimitIsUsed,
                AlphaLimitLowerLimit = suitabilityParameters.AlphaLimitLowerLimit,
                AlphaLimitUpperLimit = suitabilityParameters.AlphaLimitUpperLimit,
                AlphaLimitRelativeStDevPercent = suitabilityParameters.AlphaLimitRelativeStDevPercent,
                SignalToNoiseLimitIsUsed = suitabilityParameters.SignalToNoiseLimitIsUsed,
                SignalToNoiseLimitLowerLimit = suitabilityParameters.SignalToNoiseLimitLowerLimit,
                SignalToNoiseLimitUpperLimit = suitabilityParameters.SignalToNoiseLimitUpperLimit,
                SignalToNoiseLimitRelativeStDevPercent = suitabilityParameters.SignalToNoiseLimitRelativeStDevPercent,
                PeakWidthLimitIsUsed = suitabilityParameters.PeakWidthLimitIsUsed,
                PeakWidthLimitLowerLimit = suitabilityParameters.PeakWidthLimitLowerLimit,
                PeakWidthLimitUpperLimit = suitabilityParameters.PeakWidthLimitUpperLimit,
                PeakWidthLimitRelativeStDevPercent = suitabilityParameters.PeakWidthLimitRelativeStDevPercent

            };
            return suitabilityParameters16;
        }
    }
}
