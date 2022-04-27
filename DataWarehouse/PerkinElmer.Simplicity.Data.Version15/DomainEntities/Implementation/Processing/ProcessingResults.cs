using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    internal class ProcessingResults : IProcessingResults
    {
        public IList<IRunPeakResult> RunResults { get; set; }

        public IDictionary<Guid, IDictionary<Guid, ISuitabilityResult>> SuitabilityResults { get; set; } =
            new Dictionary<Guid, IDictionary<Guid, ISuitabilityResult>>();

        public IDictionary<Guid /*CompoundGuid*/, ICompoundSuitabilitySummaryResults> CompoundSuitabilitySummaryResults { get; set; } =
            new Dictionary<Guid, ICompoundSuitabilitySummaryResults>();

        public IList<Guid> BatchRunChannelsWithTooManyPeaks { get; set; } = new List<Guid>();
    }
}