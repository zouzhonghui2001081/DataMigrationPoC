using CalculatedChannelData15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalculatedChannelData;
using CalculatedChannelData16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalculatedChannelData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class CalculatedChannelData
    {
        public static CalculatedChannelData16 Transform(
            CalculatedChannelData15 calculatedChannelData)
        {
            if (calculatedChannelData == null) return null;
            return new CalculatedChannelData16
            {
                BatchRunAnalysisResultId = calculatedChannelData.BatchRunAnalysisResultId,
                Id = calculatedChannelData.Id,
                BatchRunChannelGuid = calculatedChannelData.BatchRunChannelGuid,
                ChannelDataType = calculatedChannelData.ChannelDataType,
                ChannelType = calculatedChannelData.ChannelType,
                ChannelIndex = calculatedChannelData.ChannelIndex,
                ChannelMetaData = calculatedChannelData.ChannelMetaData,
                RawChannelType = calculatedChannelData.RawChannelType,
                BlankSubtractionApplied = calculatedChannelData.BlankSubtractionApplied,
                SmoothApplied = calculatedChannelData.SmoothApplied
            };
        }
    }
}
