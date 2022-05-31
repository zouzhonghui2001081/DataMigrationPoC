using BatchRunChannelMap15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.BatchRunChannelMap;
using BatchRunChannelMap16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.BatchRunChannelMap;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class BatchRunChannelMap
    {
        public static BatchRunChannelMap16 Transform(BatchRunChannelMap15 batchRunChannelMap)
        {
            if (batchRunChannelMap == null) return null;
            return new BatchRunChannelMap16
            {
                Id = batchRunChannelMap.Id,
                AnalysisResultSetId = batchRunChannelMap.AnalysisResultSetId,
                BatchRunChannelGuid = batchRunChannelMap.BatchRunChannelGuid,
                BatchRunGuid = batchRunChannelMap.BatchRunGuid,
                OriginalBatchRunGuid = batchRunChannelMap.OriginalBatchRunGuid,
                BatchRunChannelDescriptorType = batchRunChannelMap.BatchRunChannelDescriptorType,
                BatchRunChannelDescriptor = batchRunChannelMap.BatchRunChannelDescriptor,
                ProcessingMethodGuid = batchRunChannelMap.ProcessingMethodGuid,
                ProcessingMethodChannelGuid = batchRunChannelMap.ProcessingMethodChannelGuid,
                XData = batchRunChannelMap.XData,
                YData = batchRunChannelMap.YData
            };
        }
    }
}
