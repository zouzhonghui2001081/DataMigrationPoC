using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
	public interface IPostProcessingMetaData
	{
		Guid SubtractedBlankOrigBatchRunGuid { get; set; }
	}
}