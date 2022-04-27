using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class BrChannelsWithExceededNumberOfPeaks
	{
		public long Id { get; set; }
		public long AnalysisResultSetId { get; set; }
		public Guid BatchRunChannelGuid { get; set; }
	}
}
