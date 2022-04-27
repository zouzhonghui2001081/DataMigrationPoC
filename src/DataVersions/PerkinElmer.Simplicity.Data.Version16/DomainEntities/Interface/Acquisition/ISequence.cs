using System.Collections.Generic;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface ISequence
	{
		ISequenceInfo Info { get; set; }

		ISequenceSampleInfo[] Samples { get; set; }

		IBatchRun[] ExternalBaselineRuns { get; set; }

		IList<IAcquisitionMethod> ExternalBaselineRunsAcquisitionMethods { get; set; }

		IList<IProcessingMethod> ExternalBaselineRunsProcessingMethods { get; set; }
	}
}