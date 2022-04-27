using System;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface IVirtualBatchRun
	{
		IBatchRun OriginalBatchRun { get; set; } // Read-only we never modify it

		IBatchResultSetInfo OriginalBatchResultSetInfo { get; set; }	// Read-only we never modify it

		Guid ModifiableProcessingMethodGuid { get; set; } 

		IBatchRunInfo ModifiableBatchRunInfo { get; set; }

        Guid[] CalculatedBatchRunChannelGuids { get; set; } // Blank subtracted XY data (seen as additional channel), may be also smoothed data channel (TBD)
	}
}