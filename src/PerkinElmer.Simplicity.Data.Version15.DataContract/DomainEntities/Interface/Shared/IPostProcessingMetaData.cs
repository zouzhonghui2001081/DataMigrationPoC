using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
	public interface IPostProcessingMetaData
	{
		Guid SubtractedBlankOrigBatchRunGuid { get; set; }
	}
}