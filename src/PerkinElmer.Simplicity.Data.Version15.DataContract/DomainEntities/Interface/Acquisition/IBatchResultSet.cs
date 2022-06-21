using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
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