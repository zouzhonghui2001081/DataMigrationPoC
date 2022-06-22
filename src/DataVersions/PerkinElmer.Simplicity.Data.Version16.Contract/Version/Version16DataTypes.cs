using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.Version
{
    [Flags]
    public enum Version16DataTypes
    {
        Project = 1,
        AcqusitionMethod = 2,
        Sequence = 4,
        AnalysisResultSet = 8,
        BatchResultSet = 16,
        ProcessingMethod = 32,
        CompoundLibrary = 64,
        ReportTemplate = 128,
        ReviewApprove = 256,
    }

}
