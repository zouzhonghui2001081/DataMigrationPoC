using PdaWavelengthMaxParameters15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod.PdaWavelengthMaxParameters;
using PdaWavelengthMaxParameters16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod.PdaWavelengthMaxParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaWavelengthMaxParameters
    {
        public static PdaWavelengthMaxParameters16 Transform(
            PdaWavelengthMaxParameters15 pdaWavelengthMaxParameters)
        {
            if (pdaWavelengthMaxParameters == null) return null;
            return new PdaWavelengthMaxParameters16
            {
                Id = pdaWavelengthMaxParameters.Id,
                ChannelMethodId = pdaWavelengthMaxParameters.ChannelMethodId,
                MinWavelength = pdaWavelengthMaxParameters.MinWavelength,
                MaxWavelength = pdaWavelengthMaxParameters.MaxWavelength,
                ApplyBaselineCorrection = pdaWavelengthMaxParameters.ApplyBaselineCorrection,
                UseAutoAbsorbanceThreshold = pdaWavelengthMaxParameters.UseAutoAbsorbanceThreshold,
                ManualAbsorbanceThreshold = pdaWavelengthMaxParameters.ManualAbsorbanceThreshold
            };
        }
    }
}
