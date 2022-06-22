using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Acquisition
{
    internal class BatchResultSet : IBatchResultSet
    {
        public IBatchResultSetInfo Info { get; set; }

        public ISequence Sequence { get; set; }

        public IBatchRunWithRawData[] BatchRuns { get; set; }
        public IBatchRunWithRawData[] ExternalBaselineRuns { get; set; }
        public IList<IAcquisitionMethod> AcquisitionMethods { get; set; }
        public IList<IProcessingMethod> ProcessingMethods { get; set; }
    }
}
