using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class BatchRunChannelMap
	{
		public long Id { get; set; }
		public long AnalysisResultSetId { get; set; }
        public Guid BatchRunChannelGuid { get; set; }
        public Guid BatchRunGuid { get; set; }
        public Guid OriginalBatchRunGuid { get; set; }
        public string BatchRunChannelDescriptorType { get; set; }
        public string BatchRunChannelDescriptor { get; set; }
        public Guid ProcessingMethodGuid { get; set; }
        public Guid ProcessingMethodChannelGuid { get; set; }
        public double[] XData { get; set; }
        public double[] YData { get; set; }
	}
}