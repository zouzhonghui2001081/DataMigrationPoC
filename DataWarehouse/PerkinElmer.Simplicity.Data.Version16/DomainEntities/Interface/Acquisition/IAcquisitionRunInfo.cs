using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IAcquisitionRunInfo
    {
        AcquisitionCompletionState AcquisitionCompletionState { get; set; }
        string AcquisitionCompletionStateDetails { get; set; }
        DateTime AcquisitionTime { get; set; }
        bool IsModifiedAfterSubmission { get; set; }
    }
}
