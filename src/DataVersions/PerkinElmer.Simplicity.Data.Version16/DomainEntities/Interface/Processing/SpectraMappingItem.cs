using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public class SpectraMappingItem
	{
		public Guid SpectrumGuid { get; set; }
		public double Time { get; set; }
		public Guid BatchRunGuid { get; set; }
		public Guid ProcessingMethodGuid { get; set; }
	}
}
