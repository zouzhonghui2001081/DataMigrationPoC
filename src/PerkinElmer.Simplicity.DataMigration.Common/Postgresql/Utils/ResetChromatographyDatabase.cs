using System;
using System.Configuration;
using System.Data;
using Dapper;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils.Resources;

namespace PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils
{
    public class ResetChromatographyDatabase : ResetDatabaseBase
    {
        public ErrorCode Reset(Mode mode, ReleaseVersions releaseVersion)
        {
            var connectionStringBuilder = GenerateConnnectionStringBuilder(releaseVersion);
            switch (mode)
            {
                case Mode.Developer:
                    return ResetDeveloper(releaseVersion, connectionStringBuilder);
                case Mode.Production:
                    return ResetProduction(releaseVersion, connectionStringBuilder);
                case Mode.Hard:
                    return ResetHard(releaseVersion, connectionStringBuilder);
                default:
                    return ErrorCode.IncorrectCmdLineArg;
            }
        }

        private ErrorCode ResetProduction(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            try
            {
                var schemaVersionTable = GetSchemaVersionTableName(releaseVersion);
                var returnValue = IsDatabaseExist(connectionBuilder.Database);
                if (returnValue.Item1.Equals(false) && returnValue.Item2.Equals(ErrorCode.ChromatographyDbDoesNotExists))
                {
                    CreateDatabase(releaseVersion, connectionBuilder); // Database not found - create it
                    return ErrorCode.NoError;
                }
                if (returnValue.Item1.Equals(false) && returnValue.Item2.Equals(ErrorCode.NpgSqlConnectionFailure))
                {
                    return ErrorCode.NpgSqlConnectionFailure;
                }

                if (!DatabaseSchemaExists(connectionBuilder.ConnectionString, schemaVersionTable))
                {
                    return ErrorCode.ChromatographySchemaDoesNotExists;
                }
                return ErrorCode.ChromatographyDbExists;
            }
            catch (NpgsqlException ex)
            {
                Log.Error("Error in ResetProduction method", ex);
                return ErrorCode.NpgSqlConnectionFailure;
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetProduction method", ex);
                return ErrorCode.ChromatographyDbError;
            }
        }

        private ErrorCode ResetDeveloper(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            try
            {
                var returnValue = IsDatabaseExist(connectionBuilder.Database);
                if (returnValue.Item1.Equals(false) && returnValue.Item2.Equals(ErrorCode.ChromatographyDbDoesNotExists))
                {
                    CreateDatabase(releaseVersion, connectionBuilder); // Database not found - create it
                    return ErrorCode.NoError;
                }
                if (returnValue.Item1.Equals(false) && returnValue.Item2.Equals(ErrorCode.NpgSqlConnectionFailure))
                {
                    return ErrorCode.NpgSqlConnectionFailure;
                }
                if (IsSchemaOrDataVersionDifferent(releaseVersion, connectionBuilder))
                {
                    DropDatabase(connectionBuilder.Database);
                    CreateDatabase(releaseVersion, connectionBuilder);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetDeveloper", ex);
                return ErrorCode.ChromatographyDbError;
            }
            return ErrorCode.NoError;
        }

        private ErrorCode ResetHard(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            try
            {
                DropDatabase(connectionBuilder.Database);
                CreateDatabase(releaseVersion, connectionBuilder);
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetDeveloper", ex);
                return ErrorCode.ChromatographyDbError;
            }
            return ErrorCode.NoError;
        }

        private void CreateDatabase(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            var dbSchemaResourceName = GetDbSchemaResourceName(releaseVersion);
            var notificationFunctionTriggersResourceName = GetNotificationFunctionTriggersResourceName(releaseVersion);
            var dummyDataResourceName = GetDummyDataResourceName(releaseVersion);
            InitializeDatabase(connectionBuilder.Database);
            using (var dbConnection = new NpgsqlConnection(connectionBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(releaseVersion,dbSchemaResourceName));
                InsertSchemaVersion(releaseVersion, dbConnection);
                InsertDataVersion(releaseVersion, dbConnection);
                // Insert dummy data
                dbConnection.Execute(GetSqlScript(releaseVersion,dummyDataResourceName));
                dbConnection.Execute(GetSqlScript(releaseVersion,notificationFunctionTriggersResourceName));
                dbConnection.Close();
            }
            Log.Info($"Created database {connectionBuilder.Database}");
        }

        private bool IsSchemaOrDataVersionDifferent(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            var schemaVersionTableName = GetSchemaVersionTableName(releaseVersion);
            var targeSchemaVersion = GetSchemaVersion(releaseVersion);
            var targetDataVersion = GetDataVersion(releaseVersion);
            using (var connection = new NpgsqlConnection(connectionBuilder.ConnectionString))
            {
                connection.Open();
                // Schema version
                var schemaVersion = connection.QueryFirstOrDefault(
                    "SELECT MajorVersion,MinorVersion " +
                    $"FROM {schemaVersionTableName} " +
                    "WHERE MajorVersion != @DataMajorVersion",
                    new { DataMajorVersion = -1 });

                if (schemaVersion == null)
                    return true;

                if ((int)schemaVersion.majorversion != targeSchemaVersion.Major ||
                    (int)schemaVersion.minorversion != targeSchemaVersion.Minor)
                {
                    return true; // Schema version different
                }

                // Data version
                var dataVersion =
                    connection.QueryFirstOrDefault(
                        "SELECT MinorVersion " +
                        $"FROM {schemaVersionTableName} " +
                        "WHERE MajorVersion = @DataMajorVersion;",
                        new { DataMajorVersion = -1 });

                if (dataVersion == null)
                    return true;

                if ((int)dataVersion.minorversion != targetDataVersion.minorVersion)
                {
                    return true; // Data version different
                }

                connection.Close();
                return false;
            }
        }

        private void InsertSchemaVersion(ReleaseVersions releaseVersion, IDbConnection dbConnection)
        {
            Log.Info("InsertCurrentSchemaVersion() called");

            try
            {
                var schemaVersionTableName = GetSchemaVersionTableName(releaseVersion);
                var schemaVersion = GetSchemaVersion(releaseVersion);
                dbConnection.Execute(
                    $"INSERT INTO {schemaVersionTableName}(MajorVersion, MinorVersion) Values({schemaVersion.Major}, {schemaVersion.Minor});");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in schema version update", ex);
                throw;
            }
        }

        private void InsertDataVersion(ReleaseVersions releaseVersion, IDbConnection dbConnection)
        {
            Log.Info("InsertCurrentDataVersion() called");

            try
            {
                var schemaVersionTableName = GetSchemaVersionTableName(releaseVersion);
                var dataVersion = GetDataVersion(releaseVersion);
                dbConnection.Execute(
                    $"INSERT INTO {schemaVersionTableName}(MajorVersion, MinorVersion) Values({dataVersion.majorVersion}, {dataVersion.minorVersion});");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in InsertCurrentDataVersion()", ex);
                throw;
            }
        }

        private NpgsqlConnectionStringBuilder GenerateConnnectionStringBuilder(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    var chromatographyConnNameV15 = ConfigurationManager.AppSettings["ChromatographyConnVer15"];
                    var connectionStringVer15 = ConfigurationManager.ConnectionStrings[chromatographyConnNameV15].ConnectionString;
                    return new NpgsqlConnectionStringBuilder(connectionStringVer15);
                case ReleaseVersions.Version16:
                    var chromatographyConnNameV16 = ConfigurationManager.AppSettings["ChromatographyConnVer16"];
                    var connectionStringVer16 = ConfigurationManager.ConnectionStrings[chromatographyConnNameV16].ConnectionString;
                    return new NpgsqlConnectionStringBuilder(connectionStringVer16);
            }

            throw new NotImplementedException();
        }

        private string GetDbSchemaResourceName(ReleaseVersions releaseVersion)
        {
            switch(releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return ChromatographyResourceNames.ChromatographyDbSchemaVer15;
                case ReleaseVersions.Version16:
                    return ChromatographyResourceNames.ChromatographyDbSchemaVer16;
            }

            throw new NotImplementedException();
        }

        private string GetNotificationFunctionTriggersResourceName(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return ChromatographyResourceNames.ChromatographyNotificationFunctionTriggersVer15;
                case ReleaseVersions.Version16:
                    return ChromatographyResourceNames.ChromatographyNotificationFunctionTriggersVer16;
            }
            throw new NotImplementedException();
        }

        private string GetDummyDataResourceName(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return ChromatographyResourceNames.ChromatographyDummyDataVer15;
                case ReleaseVersions.Version16:
                    return ChromatographyResourceNames.ChromatographyDummyDataVer16;
            }

            throw new NotImplementedException();
        }

        private Version GetSchemaVersion(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return SchemaVersions.ChromatographySchemaVersion15;
                case ReleaseVersions.Version16:
                    return SchemaVersions.ChromatographySchemaVersion16;
            }
            throw new NotImplementedException();
        }

        private (int majorVersion, int minorVersion) GetDataVersion(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return (DataVersions.ChromatographyDataVersion15Major,
                        DataVersions.ChromatographyDataVersion15Minor);
                case ReleaseVersions.Version16:
                    return (DataVersions.ChromatographyDataVersion16Major,
                        DataVersions.ChromatographyDataVersion16Minor);
            }
            throw new NotImplementedException();
        }

        private string GetSchemaVersionTableName(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                case ReleaseVersions.Version16:
                    return ChromatographyResourceNames.SchemaVersionTable;
            }
            throw new NotImplementedException();
        }
    }
}
