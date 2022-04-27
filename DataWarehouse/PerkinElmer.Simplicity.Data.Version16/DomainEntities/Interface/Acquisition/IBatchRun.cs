using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Domain.Contracts.Acquisition
{
	public interface IBatchRun : IBatchRunBase
	{
		Guid[] BatchRunChannelGuids { get; set; }
	}
}