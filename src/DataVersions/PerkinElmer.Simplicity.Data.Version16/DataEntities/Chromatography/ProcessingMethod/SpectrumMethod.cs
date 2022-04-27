
using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod
{
    public class SpectrumMethod
	{
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public Guid Guid { get; set; }
		public double StartRetentionTime { get; set; }
		public double EndRetentionTime { get; set; }
		public int BaselineCorrectionType { get; set; }
		public double BaselineStartRetentionTime { get; set; }
		public double BaselineEndRetentionTime { get; set; }
		
	}
}
