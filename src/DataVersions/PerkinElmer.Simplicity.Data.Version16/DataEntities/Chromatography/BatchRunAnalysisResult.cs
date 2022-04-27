using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class BatchRunAnalysisResult
    {
        public long Id { get; set; }

        public long AnalysisResultSetId { get; set; }

        public long SequenceSampleInfoBatchResultId { get; set; }

        public long ProjectId { get; set; }

        public Guid OriginalBatchResultSetGuid { get; set; }

        public Guid ModifiableBatchRunInfoGuid { get; set; }

        public Guid OriginalBatchRunInfoGuid { get; set; }

        public long BatchRunId { get; set; }

        public string BatchRunName { get; set; }

        public DateTime BatchRunCreatedDate { get; set; }

        public string BatchRunCreatedUserId { get; set; }

        public DateTime BatchRunModifiedDate { get; set; }

        public string BatchRunModifiedUserId { get; set; }

        public long SequenceSampleInfoModifiableId { get; set; }

	    public long ProcessingMethodModifiableId { get; set; }
	    public long CalibrationMethodModifiableId { get; set; }

	    public bool IsBlankSubtractor { get; set; }
	    public short DataSourceType { get; set; }
	}
}
