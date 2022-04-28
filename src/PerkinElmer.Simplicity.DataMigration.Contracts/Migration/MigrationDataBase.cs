
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
        public abstract MigrationVersions MigrationVersion { get; }

        public abstract MigrationDataTypes MigrationDataTypes { get; }
    }
}
