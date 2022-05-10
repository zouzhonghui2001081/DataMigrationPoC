using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
    internal class AcquisitionRunInfo : IAcquisitionRunInfo
    {
        public AcquisitionCompletionState AcquisitionCompletionState { get; set; }
        public string AcquisitionCompletionStateDetails { get; set; }
        public DateTime AcquisitionTime { get; set; }
        public bool IsModifiedAfterSubmission { get; set; }
    }
}
