using System;
using System.Collections.Generic;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	public class ChannelMappingItem : IInternalChannelMappingItem
	{
		// Data side
		public Guid BatchRunChannelGuid { get; set; }
	
		public Guid BatchRunGuid { get; set; }
		
		public Guid OriginalBatchRunGuid { get; set; }

		public IChromatographicChannelDescriptor BatchRunChannelDescriptor { get; set; }

		// PM side
		public Guid ProcessingMethodChannelGuid { get; set; }
		
		public Guid ProcessingMethodGuid { get; set; }
        
        // Cache of XYData
		public (IList<double> TimeInSeconds, IList<double> Response) XyData { get; set; }
	}
}
