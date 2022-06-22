using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
	public interface IPostProcessingMetaData
	{
		Guid SubtractedBlankOrigBatchRunGuid { get; set; }
	}
}