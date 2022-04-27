using PdaApexOptimizedParameters15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.PdaApexOptimizedParameters;
using PdaApexOptimizedParameters16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.PdaApexOptimizedParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaApexOptimizedParameters
    {
        public static PdaApexOptimizedParameters16 Transform(
            PdaApexOptimizedParameters15 pdaApexOptimizedParameters)
        {
            if (pdaApexOptimizedParameters == null) return null;
            return new PdaApexOptimizedParameters16
            {
                Id = pdaApexOptimizedParameters.Id,
                ProcessingMethodId = pdaApexOptimizedParameters.ProcessingMethodId,
                MinWavelength = pdaApexOptimizedParameters.MinWavelength,
                MaxWavelength = pdaApexOptimizedParameters.MaxWavelength,
                WavelengthBandwidth = pdaApexOptimizedParameters.WavelengthBandwidth,
                UseReference = pdaApexOptimizedParameters.UseReference,
                ReferenceWavelength = pdaApexOptimizedParameters.ReferenceWavelength,
                ReferenceWavelengthBandwidth = pdaApexOptimizedParameters.ReferenceWavelengthBandwidth,
                ApplyBaselineCorrection = pdaApexOptimizedParameters.ApplyBaselineCorrection,
                UseAutoAbsorbanceThreshold = pdaApexOptimizedParameters.UseAutoAbsorbanceThreshold,
                ManualAbsorbanceThreshold = pdaApexOptimizedParameters.ManualAbsorbanceThreshold
            };
        }
    }
}
