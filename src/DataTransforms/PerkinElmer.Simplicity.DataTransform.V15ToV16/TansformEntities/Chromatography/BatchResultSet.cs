using BatchResultSet15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.BatchResultSet;
using BatchResultSet16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.BatchResultSet;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class BatchResultSet
    {
        public static BatchResultSet16 Transform(BatchResultSet15 batchResultSet)
        {
            if (batchResultSet == null) return null;
            var batchResultSet16 = new BatchResultSet16
            {
                Id = batchResultSet.Id,
                ProjectId = batchResultSet.ProjectId,
                Guid = batchResultSet.Guid,
                CreatedDate = batchResultSet.CreatedDate,
                CreatedUserId = batchResultSet.CreatedUserId,
                ModifiedDate = batchResultSet.ModifiedDate,
                ModifiedUserId = batchResultSet.ModifiedUserId,
                IsCompleted = batchResultSet.IsCompleted,
                Name = batchResultSet.Name,
                DataSourceType = batchResultSet.DataSourceType,
                InstrumentMasterId = batchResultSet.InstrumentMasterId,
                InstrumentId = batchResultSet.InstrumentId,
                InstrumentName = batchResultSet.InstrumentName,
                Regulated = batchResultSet.Regulated
            };
            if (batchResultSet.SequenceSampleInfos != null)
            {
                foreach (var sequenceSampleInfo in batchResultSet.SequenceSampleInfos)
                    batchResultSet16.SequenceSampleInfos.Add(SequenceSampleInfoBatchResult.Transform(sequenceSampleInfo));
            }

            return batchResultSet16;
        }
    }
}
