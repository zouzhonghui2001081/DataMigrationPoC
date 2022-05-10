using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface IBatchRun : IBatchRunBase
	{
		Guid[] BatchRunChannelGuids { get; set; }
	}
}