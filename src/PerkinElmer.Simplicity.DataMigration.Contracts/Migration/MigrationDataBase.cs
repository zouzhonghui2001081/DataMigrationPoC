
using System;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    [Flags]
    public enum MigrationDataTypes
    {
        Project = 1,
        AcqusitionMethod = 2,
        Sequence = 4,
        AnlysisResultSet = 8,
        BatchResultSet = 16,
        ProcessingMethod = 32,
        CompoundLibrary = 64,
        ReportTemplate = 128,
        ReviewApprove = 256,
    }

    public abstract class MigrationDataBase
    {
        public abstract MigrationVersions MigrationVersion { get; }

        public abstract MigrationDataTypes MigrationDataTypes { get; }
    }
}
