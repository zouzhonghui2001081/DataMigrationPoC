using System;
using System.Collections.Generic;
using PerkinElmer.Domain.Contracts.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface IProcessingResults
	{
		IList<IRunPeakResult> RunResults { get; set; }
		
		IDictionary<Guid /*BrChannelGuid*/, IDictionary<Guid /*CompoundGuid*/, ISuitabilityResult>> SuitabilityResults { get; set; } 

		IDictionary<Guid /*CompoundGuid*/, ICompoundSuitabilitySummaryResults> CompoundSuitabilitySummaryResults { get; set; } 

	    IList<Guid> BatchRunChannelsWithTooManyPeaks { get; set; }
	}
}
