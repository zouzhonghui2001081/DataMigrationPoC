using System.Collections.Generic;
using BatchRun15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.BatchRun;
using BatchRun16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.BatchRun;
using StreamDataBatchResult16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.StreamDataBatchResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class BatchRun
    {
        public static BatchRun16 Transform(BatchRun15 batchRun)
        {
            if (batchRun == null) return null;
            var batchRun16 = new BatchRun16
            {
                Id = batchRun.Id,
                Name = batchRun.Name,
                Guid = batchRun.Guid,
                CreatedDate = batchRun.CreatedDate,
                CreatedUserId = batchRun.CreatedUserId,
                ModifiedDate = batchRun.ModifiedDate,
                ModifiedUserId = batchRun.ModifiedUserId,
                IsBaselineRun = batchRun.IsBaselineRun,
                AcquisitionCompletionState = batchRun.AcquisitionCompletionState,
                AcquisitionTime = batchRun.AcquisitionTime,
                RepeatIndex = batchRun.RepeatIndex,
                SequenceSampleInfoBatchResultId = batchRun.SequenceSampleInfoBatchResultId,
                BatchResultSetId = batchRun.BatchResultSetId,
                ProcessingMethodBatchResultId = batchRun.ProcessingMethodBatchResultId,
                CalibrationMethodBatchResultId = batchRun.CalibrationMethodBatchResultId,
                AcquisitionMethodBatchResultId = batchRun.AcquisitionMethodBatchResultId,
                DataSourceType = batchRun.DataSourceType,
                IsModifiedAfterSubmission = batchRun.IsModifiedAfterSubmission,
                AcquisitionCompletionStateDetails = batchRun.AcquisitionCompletionStateDetails
            };
            if (batchRun.StreamDataList == null) return batchRun16;
            batchRun16.StreamDataList = new List<StreamDataBatchResult16>();
            foreach (var streamData in batchRun.StreamDataList)
                batchRun16.StreamDataList.Add(StreamDataBatchResult.Transform(streamData));

            return batchRun16;
        }
    }
}
