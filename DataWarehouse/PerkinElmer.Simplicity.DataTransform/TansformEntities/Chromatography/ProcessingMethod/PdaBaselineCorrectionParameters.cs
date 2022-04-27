using PdaBaselineCorrectionParameters15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.PdaBaselineCorrectionParameters;
using PdaBaselineCorrectionParameters16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.PdaBaselineCorrectionParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaBaselineCorrectionParameters
    {
        public static PdaBaselineCorrectionParameters16 Transform(
            PdaBaselineCorrectionParameters15 pdaBaselineCorrectionParameters)
        {
            if (pdaBaselineCorrectionParameters == null) return null;

            return new PdaBaselineCorrectionParameters16
            {
                Id = pdaBaselineCorrectionParameters.Id,
                ChannelMethodId = pdaBaselineCorrectionParameters.ChannelMethodId,
                CorrectionType = pdaBaselineCorrectionParameters.CorrectionType,
                SelectedSpectrumTime = pdaBaselineCorrectionParameters.SelectedSpectrumTime,
                RangeStart = pdaBaselineCorrectionParameters.RangeStart,
                RangeEnd = pdaBaselineCorrectionParameters.RangeEnd
            };
        }
    }
}
