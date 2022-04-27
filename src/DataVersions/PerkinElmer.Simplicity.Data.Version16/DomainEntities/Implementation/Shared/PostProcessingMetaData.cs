using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared
{
	public class PostProcessingMetaData : IPostProcessingMetaData
	{
		public Guid SubtractedBlankOrigBatchRunGuid { get; set; }
	}
}
