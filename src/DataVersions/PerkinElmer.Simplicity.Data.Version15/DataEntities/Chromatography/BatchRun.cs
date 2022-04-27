using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class BatchRun
    {
        public long Id { get; set; }
        public string Name { get; set; }
	    public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUserId { get; set; }
        public Boolean IsBaselineRun { get; set; }
        public short AcquisitionCompletionState { get; set; }
        public DateTime AcquisitionTime { get; set; }
        public int RepeatIndex { get; set; }
        public long SequenceSampleInfoBatchResultId { get; set; }
        public long BatchResultSetId { get; set; }
        public  long ProcessingMethodBatchResultId { get; set; }
        public long CalibrationMethodBatchResultId { get; set; }
	    public long AcquisitionMethodBatchResultId { get; set; }
	    public short DataSourceType { get; set; }
	    public bool IsModifiedAfterSubmission { get; set; }
	    public string AcquisitionCompletionStateDetails { get; set; }
		public IList<StreamDataBatchResult> StreamDataList { get; set; }
    }
}
