using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class ManualOverrideMap
	{
		public ManualOverrideMap()
		{
			ManualOverrideIntegrationEvents = new List<ManualOverrideIntegrationEvent>();
		}
		public long Id { get; set; }
		public long AnalysisResultSetId { get; set; }
		public Guid BatchRunChannelGuid { get; set; }
		public Guid BatchRunGuid { get; set; }
		public List<ManualOverrideIntegrationEvent> ManualOverrideIntegrationEvents { get; set; }
	}
}
