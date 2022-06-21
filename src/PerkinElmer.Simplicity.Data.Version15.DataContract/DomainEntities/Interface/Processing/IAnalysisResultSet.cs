using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface IAnalysisResultSet : ICloneable
	{
        IAnalysisResultSetDescriptor Descriptor { get; set; }

        IVirtualBatchRun[] BatchRuns { get; set; } // Change to list

		IBatchRun[] ExternalBaselineRuns { get; set; } // Change to list

		// Following lists for ExternalBaselineRuns, BatchRuns

		IList<IAcquisitionMethod> ReadOnlyAcquisitionMethods { get; set; }

		IList<IProcessingMethod> ReadOnlyProcessingMethods { get; set; }

        IList<IModifiableProcessingMethod> ModifiableProcessingMethods { get; set; }

        IProcessingResults Results { get; set; }
	}
}