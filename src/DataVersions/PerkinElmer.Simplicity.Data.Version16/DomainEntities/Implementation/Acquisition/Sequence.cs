using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
	internal class Sequence : ISequence
	{
		public ISequenceInfo Info { get; set; }

		public ISequenceSampleInfo[] Samples { get; set; }

		public IBatchRun[] ExternalBaselineRuns { get; set; }

		public IList<IAcquisitionMethod> ExternalBaselineRunsAcquisitionMethods { get; set; }

		public IList<IProcessingMethod> ExternalBaselineRunsProcessingMethods { get; set; }
	}
}