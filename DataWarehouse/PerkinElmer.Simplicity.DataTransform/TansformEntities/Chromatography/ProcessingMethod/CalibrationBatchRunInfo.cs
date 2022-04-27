using CalibrationBatchRunInfo15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.CalibrationBatchRunInfo;
using CalibrationBatchRunInfo16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.CalibrationBatchRunInfo;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class CalibrationBatchRunInfo
    {
        public static CalibrationBatchRunInfo16 Transform(
            CalibrationBatchRunInfo15 calibrationBatchRunInfo)
        {
            if (calibrationBatchRunInfo == null) return null;
            return new CalibrationBatchRunInfo16
            {
                Id = calibrationBatchRunInfo.Id,
                ProcessingMethodId = calibrationBatchRunInfo.ProcessingMethodId,
                Key = calibrationBatchRunInfo.Key,
                BatchRunGuid = calibrationBatchRunInfo.BatchResultSetGuid,
                BatchResultSetGuid = calibrationBatchRunInfo.BatchResultSetGuid,
                BatchRunName = calibrationBatchRunInfo.BatchRunName,
                ResultSetName = calibrationBatchRunInfo.ResultSetName,
                BatchRunAcquisitionTime = calibrationBatchRunInfo.BatchRunAcquisitionTime
            };
        }
    }
}
