using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class AnalysisResultSet : IAnalysisResultSet
	{
        public IAnalysisResultSetDescriptor Descriptor { get; set; }

        public IVirtualBatchRun[] BatchRuns { get; set; }

		public IBatchRun[] ExternalBaselineRuns { get; set; } // Change to list

		public IList<IAcquisitionMethod> ReadOnlyAcquisitionMethods { get; set; }

		public IList<IProcessingMethod> ReadOnlyProcessingMethods { get; set; }

		public IList<IModifiableProcessingMethod> ModifiableProcessingMethods { get; set; }

		public IProcessingResults Results { get; set; }
		
		public object Clone()
		{
			throw new System.NotImplementedException();
		}

		public AnalysisResultSet()
		{
			ReadOnlyAcquisitionMethods = new List<IAcquisitionMethod>();
			ReadOnlyProcessingMethods = new List<IProcessingMethod>();
		}
	}
}
