using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
	public interface IBatchRun : IBatchRunBase
	{
		Guid[] BatchRunChannelGuids { get; set; }
	}
}