using System;
using PerkinElmer.Domain.Contracts.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IBatchRunBase
    {
        IBatchRunInfo Info { get; set; }
        Guid AcquisitionMethodGuid { get; set; }
        Guid ProcessingMethodGuid { get; set; }
        Guid CalibrationMethodGuid { get; set; }
    }
}