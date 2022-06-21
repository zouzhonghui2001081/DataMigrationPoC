using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
	public class ManualOverrideMappingItem : IManualOverrideMappingItem
	{
		public Guid BatchRunChannelGuid { get; set; }
		public Guid BatchRunGuid { get; set; }
		public IList<IIntegrationEvent> TimedIntegrationParameters { get; set; }
	}
}
