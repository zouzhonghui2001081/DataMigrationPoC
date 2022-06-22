using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public class SpectraMappingItem
	{
		public Guid SpectrumGuid { get; set; }
		public double Time { get; set; }
		public Guid BatchRunGuid { get; set; }
		public Guid ProcessingMethodGuid { get; set; }
	}
}
