using BrChannelsWithExceededNumberOfPeaks15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.BrChannelsWithExceededNumberOfPeaks;
using BrChannelsWithExceededNumberOfPeaks16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.BrChannelsWithExceededNumberOfPeaks;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class BrChannelsWithExceededNumberOfPeaks
    {
        public static BrChannelsWithExceededNumberOfPeaks16 Transform(
            BrChannelsWithExceededNumberOfPeaks15 exceededNumberOfPeaks)
        {
            if (exceededNumberOfPeaks == null) return null;
            return new BrChannelsWithExceededNumberOfPeaks16
            {
                Id = exceededNumberOfPeaks.Id,
                AnalysisResultSetId = exceededNumberOfPeaks.AnalysisResultSetId,
                BatchRunChannelGuid = exceededNumberOfPeaks.BatchRunChannelGuid
            };

        }
    }
}
