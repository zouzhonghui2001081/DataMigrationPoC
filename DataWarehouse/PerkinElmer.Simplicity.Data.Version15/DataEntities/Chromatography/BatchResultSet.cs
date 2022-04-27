using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class BatchResultSet
    {
	    public long Id { get; set; }

		public long ProjectId { get; set; }

	    public Guid Guid { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedUserId { get; set; }

	    public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public short DataSourceType { get; set; }

        public string InstrumentMasterId { get; set; }

        public string InstrumentId { get; set; }

        public string InstrumentName { get; set; }

        public bool Regulated { get; set; }

		public IList<SequenceSampleInfoBatchResult> SequenceSampleInfos { get; set; }
    }
}