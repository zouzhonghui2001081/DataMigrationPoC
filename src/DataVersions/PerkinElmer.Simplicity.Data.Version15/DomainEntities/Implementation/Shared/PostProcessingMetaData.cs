using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared
{
	public class PostProcessingMetaData : IPostProcessingMetaData
	{
		public Guid SubtractedBlankOrigBatchRunGuid { get; set; }
	}
}
