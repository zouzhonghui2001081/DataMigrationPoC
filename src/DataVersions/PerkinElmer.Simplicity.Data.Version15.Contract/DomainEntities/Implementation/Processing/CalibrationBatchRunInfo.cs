using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
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