using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IChannelMappingItem
	{
		// Data side
		Guid BatchRunChannelGuid { get; set; }

		Guid BatchRunGuid { get; set; }

		Guid OriginalBatchRunGuid { get; set; }

		IChromatographicChannelDescriptor BatchRunChannelDescriptor { get; set; }

		// PM side
		Guid ProcessingMethodChannelGuid { get; set; }

		Guid ProcessingMethodGuid { get; set; }
	}
}
