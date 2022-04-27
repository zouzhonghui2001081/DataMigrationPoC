using System;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class VirtualBatchRun : IVirtualBatchRun
	{
		public IBatchRun OriginalBatchRun { get; set; }

		public IBatchResultSetInfo OriginalBatchResultSetInfo { get; set; }

		public Guid ModifiableProcessingMethodGuid { get; set; }

		public IBatchRunInfo ModifiableBatchRunInfo { get; set; }

		public Guid[] CalculatedBatchRunChannelGuids { get; set; }
	}
}