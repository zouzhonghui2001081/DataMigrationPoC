using PerkinElmer.Simplicity.DataMigration.Common;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Migration
{
    public enum MigrationDataTypes
    {
        Project,
        AcqusitionMethod,
        Sequence,
        AnlysisResultSet,
        BatchResultSet,
        ProcessingMethod,
        CompoundLibrary,
        ReviewApprove,
        ReportTemplate,
    }

    public abstract class MigrationDataBase
    {
        public abstract ReleaseVersions ReleaseVersion { get; }

        public abstract MigrationDataTypes MigrationDataTypes { get; }
    }
}
