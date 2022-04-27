using BatchRunAnalysisResult15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.BatchRunAnalysisResult;
using BatchRunAnalysisResult16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.BatchRunAnalysisResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class BatchRunAnalysisResult
    {
        public static BatchRunAnalysisResult16 Transform(
            BatchRunAnalysisResult15 batchRunAnalysisResult)
        {
            if (batchRunAnalysisResult == null) return null;
            return new BatchRunAnalysisResult16
            {
                Id = batchRunAnalysisResult.Id,
                AnalysisResultSetId = batchRunAnalysisResult.AnalysisResultSetId,
                SequenceSampleInfoBatchResultId = batchRunAnalysisResult.SequenceSampleInfoBatchResultId,
                ProjectId = batchRunAnalysisResult.ProjectId,
                OriginalBatchResultSetGuid = batchRunAnalysisResult.OriginalBatchResultSetGuid,
                ModifiableBatchRunInfoGuid = batchRunAnalysisResult.ModifiableBatchRunInfoGuid,
                OriginalBatchRunInfoGuid = batchRunAnalysisResult.OriginalBatchRunInfoGuid,
                BatchRunId = batchRunAnalysisResult.BatchRunId,
                BatchRunName = batchRunAnalysisResult.BatchRunName,
                BatchRunCreatedDate = batchRunAnalysisResult.BatchRunCreatedDate,
                BatchRunCreatedUserId = batchRunAnalysisResult.BatchRunCreatedUserId,
                BatchRunModifiedDate = batchRunAnalysisResult.BatchRunModifiedDate,
                BatchRunModifiedUserId = batchRunAnalysisResult.BatchRunModifiedUserId,
                SequenceSampleInfoModifiableId = batchRunAnalysisResult.SequenceSampleInfoModifiableId,
                ProcessingMethodModifiableId = batchRunAnalysisResult.ProcessingMethodModifiableId,
                CalibrationMethodModifiableId = batchRunAnalysisResult.CalibrationMethodModifiableId,
                IsBlankSubtractor = batchRunAnalysisResult.IsBlankSubtractor,
                DataSourceType = batchRunAnalysisResult.DataSourceType
            };
        }
    }
}
