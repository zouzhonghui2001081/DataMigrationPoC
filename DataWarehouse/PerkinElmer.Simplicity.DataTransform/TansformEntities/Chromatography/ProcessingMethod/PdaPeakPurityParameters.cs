using PdaPeakPurityParameters15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.PdaPeakPurityParameters;
using PdaPeakPurityParameters16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.PdaPeakPurityParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaPeakPurityParameters
    {
        public static PdaPeakPurityParameters16 Transform(
            PdaPeakPurityParameters15 pdaPeakPurityParameters)
        {
            if (pdaPeakPurityParameters == null) return null;
            return new PdaPeakPurityParameters16
            {
                Id = pdaPeakPurityParameters.Id,
                ChannelMethodId = pdaPeakPurityParameters.ChannelMethodId,
                MinWavelength = pdaPeakPurityParameters.MinWavelength,
                MaxWavelength = pdaPeakPurityParameters.MaxWavelength,
                MinimumDataPoints = pdaPeakPurityParameters.MinimumDataPoints,
                ApplyBaselineCorrection = pdaPeakPurityParameters.ApplyBaselineCorrection,
                PurityLimit = pdaPeakPurityParameters.PurityLimit,
                PercentOfPeakHeightForSpectra = pdaPeakPurityParameters.PercentOfPeakHeightForSpectra,
                UseAutoAbsorbanceThreshold = pdaPeakPurityParameters.UseAutoAbsorbanceThreshold,
                ManualAbsorbanceThreshold = pdaPeakPurityParameters.ManualAbsorbanceThreshold
            };
        }
    }
}
