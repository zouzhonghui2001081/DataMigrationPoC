using System;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
    internal class BatchRunWithRawData : IBatchRunWithRawData
    {
        public IBatchRunInfo Info { get; set; }
        public Guid AcquisitionMethodGuid { get; set; }
        public Guid ProcessingMethodGuid { get; set; }
        public Guid CalibrationMethodGuid { get; set; }
        public IStreamData[] StreamData { get; set; }
    }
}
