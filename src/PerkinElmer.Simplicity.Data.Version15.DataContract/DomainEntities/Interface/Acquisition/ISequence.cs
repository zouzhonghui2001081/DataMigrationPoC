using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
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