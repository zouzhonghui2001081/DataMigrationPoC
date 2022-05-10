using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class CalibrationBatchRunInfo : ICalibrationBatchRunInfo
    {
        public Guid BatchRunGuid { get; set; }
        public Guid BatchResultSetGuid { get; set; }
        public string BatchRunName { get; set; }
        public string ResultSetName { get; set; }
        public DateTime BatchRunAcquisitionTime { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}