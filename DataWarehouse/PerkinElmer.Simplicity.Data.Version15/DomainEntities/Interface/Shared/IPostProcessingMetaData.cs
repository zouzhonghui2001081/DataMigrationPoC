using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared
{
	public interface IPostProcessingMetaData
	{
		Guid SubtractedBlankOrigBatchRunGuid { get; set; }
	}
}