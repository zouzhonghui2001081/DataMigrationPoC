using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IBatchRunBase
    {
        IBatchRunInfo Info { get; set; }
        Guid AcquisitionMethodGuid { get; set; }
        Guid ProcessingMethodGuid { get; set; }
        Guid CalibrationMethodGuid { get; set; }
    }
}