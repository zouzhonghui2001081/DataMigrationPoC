namespace PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils
{
    public enum ErrorCode
    {
        NoError = 0,
        IncorrectCmdLineArg = -1,
        ChromatographyDbError = -2,
        SecurityDbError = -3,
        AuditLogDbError = -4,
        ChromatographyDbExists = -5,
        ChromatographySchemaDoesNotExists = -6,
        NpgSqlConnectionFailure = -7,
        ChromatographyDbDoesNotExists = -8,
        SimplicityChromIsRunning = -9,
        ErrorRemovingDatabaseAndFolders = -15
    }
}
