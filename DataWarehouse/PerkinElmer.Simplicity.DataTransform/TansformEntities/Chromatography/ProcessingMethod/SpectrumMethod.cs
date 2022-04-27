using SpectrumMethod15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.SpectrumMethod;
using SpectrumMethod16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.SpectrumMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class SpectrumMethod
    {
        public static SpectrumMethod16 Transform(SpectrumMethod15 spectrumMethod)
        {
            if (spectrumMethod == null) return null;
            return new SpectrumMethod16
            {
                Id = spectrumMethod.Id,
                ProcessingMethodId = spectrumMethod.ProcessingMethodId,
                Guid = spectrumMethod.Guid,
                StartRetentionTime = spectrumMethod.StartRetentionTime,
                EndRetentionTime = spectrumMethod.EndRetentionTime,
                BaselineCorrectionType = spectrumMethod.BaselineCorrectionType,
                BaselineStartRetentionTime = spectrumMethod.BaselineStartRetentionTime,
                BaselineEndRetentionTime = spectrumMethod.BaselineEndRetentionTime
            };
        }
    }
}
