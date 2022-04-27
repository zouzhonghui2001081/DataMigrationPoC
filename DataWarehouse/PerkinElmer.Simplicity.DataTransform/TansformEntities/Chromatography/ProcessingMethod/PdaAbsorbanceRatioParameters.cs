using PdaAbsorbanceRatioParameters15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.PdaAbsorbanceRatioParameters;
using PdaAbsorbanceRatioParameters16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.PdaAbsorbanceRatioParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaAbsorbanceRatioParameters
    {
        public static PdaAbsorbanceRatioParameters16 Transform(
            PdaAbsorbanceRatioParameters15 pdaAbsorbanceRatioParameters)
        {
            if (pdaAbsorbanceRatioParameters == null) return null;
            return new PdaAbsorbanceRatioParameters16
            {
                Id = pdaAbsorbanceRatioParameters.Id,
                ChannelMethodId = pdaAbsorbanceRatioParameters.ChannelMethodId,
                WavelengthA = pdaAbsorbanceRatioParameters.WavelengthA,
                WavelengthB = pdaAbsorbanceRatioParameters.WavelengthB,
                ApplyBaselineCorrection = pdaAbsorbanceRatioParameters.ApplyBaselineCorrection,
                UseAutoAbsorbanceThreshold = pdaAbsorbanceRatioParameters.UseAutoAbsorbanceThreshold,
                ManualAbsorbanceThreshold = pdaAbsorbanceRatioParameters.ManualAbsorbanceThreshold
            };
        }
    }
}
