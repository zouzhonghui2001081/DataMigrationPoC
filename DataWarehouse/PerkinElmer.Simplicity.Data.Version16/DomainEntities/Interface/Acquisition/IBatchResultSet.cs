using System.Collections.Generic;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface IBatchResultSet
	{
		IBatchResultSetInfo Info { get; set; }

		IBatchRunWithRawData[] BatchRuns { get; set; }

		IBatchRunWithRawData[] ExternalBaselineRuns { get; set; }

		IList<IAcquisitionMethod> AcquisitionMethods { get; set; }

		IList<IProcessingMethod> ProcessingMethods { get; set; }
	}
}