using StreamDataBatchResult15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.StreamDataBatchResult;
using StreamDataBatchResult16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.StreamDataBatchResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class StreamDataBatchResult
    {
        public static StreamDataBatchResult16 Transform(
            StreamDataBatchResult15 streamDataBatchResult)
        {
            if (streamDataBatchResult == null) return null;
            return new StreamDataBatchResult16
            {
                BatchRunId = streamDataBatchResult.BatchRunId,
                Id = streamDataBatchResult.Id,
                StreamIndex = streamDataBatchResult.StreamIndex,
                MetaData = streamDataBatchResult.MetaData,
                MetaDataType = streamDataBatchResult.MetaDataType,
                YData = streamDataBatchResult.YData,
                DeviceId = streamDataBatchResult.DeviceId,
                LargeObjectOid = streamDataBatchResult.LargeObjectOid,
                UseLargeObjectStream = streamDataBatchResult.UseLargeObjectStream,
                FirmwareVersion = streamDataBatchResult.FirmwareVersion,
                SerialNumber = streamDataBatchResult.SerialNumber,
                ModelName = streamDataBatchResult.ModelName,
                UniqueIdentifier = streamDataBatchResult.UniqueIdentifier,
                InterfaceAddress = streamDataBatchResult.InterfaceAddress
            };
        }
    }
}
