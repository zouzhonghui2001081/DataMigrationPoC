using PdaStandardConfirmationParameters15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod.PdaStandardConfirmationParameters;
using PdaStandardConfirmationParameters16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod.PdaStandardConfirmationParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaStandardConfirmationParameters
    {
        public static PdaStandardConfirmationParameters16 Transform(
            PdaStandardConfirmationParameters15 pdaStandardConfirmationParameters)
        {
            if (pdaStandardConfirmationParameters == null) return null;
            var pdaStandardConfirmationParameters16 = new PdaStandardConfirmationParameters16
            {
                PdaStandardConfirmationGuid = pdaStandardConfirmationParameters.PdaStandardConfirmationGuid,
                Id = pdaStandardConfirmationParameters.Id,
                ChannelMethodId = pdaStandardConfirmationParameters.ChannelMethodId,
                MinWavelength = pdaStandardConfirmationParameters.MinWavelength,
                MaxWavelength = pdaStandardConfirmationParameters.MaxWavelength,
                MinimumDataPoints = pdaStandardConfirmationParameters.MinimumDataPoints,
                PassThreshold = pdaStandardConfirmationParameters.PassThreshold,
                ApplyBaselineCorrection = pdaStandardConfirmationParameters.ApplyBaselineCorrection,
                UseAutoAbsorbanceThresholdForSample = pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForSample,
                ManualAbsorbanceThresholdForSample = pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForSample,
                UseAutoAbsorbanceThresholdForStandard = pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForStandard,
                ManualAbsorbanceThresholdForStandard = pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForStandard,
                StandardType = pdaStandardConfirmationParameters.StandardType
            };
            return pdaStandardConfirmationParameters16;
        }
    }
}
