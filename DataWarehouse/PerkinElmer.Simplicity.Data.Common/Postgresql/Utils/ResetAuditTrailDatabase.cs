

using System;
using System.Configuration;
using System.Data;
using Dapper;
using Npgsql;
using PerkinElmer.Simplicity.Data.Common.Postgresql.Utils.Resources;

namespace PerkinElmer.Simplicity.Data.Common.Postgresql.Utils
{
    public class ResetAuditTrailDatabase : ResetDatabaseBase
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
                var returnValue = IsDatabaseExist(connectionBuilder.Database);

                if (returnValue.Item1.Equals(false))
                {
                    CreateDatabase(releaseVersion, connectionBuilder); // Database not found - create it
                }
            }
            catch (NpgsqlException ex)
            {
                Log.Error("Error in ResetProduction method", ex);
                return ErrorCode.NpgSqlConnectionFailure;
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetProduction method", ex);
                return ErrorCode.AuditLogDbError;
            }

            return ErrorCode.NoError;
        }

        private ErrorCode ResetDeveloper(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            try
            {
                var returnValue = IsDatabaseExist(connectionBuilder.Database);

                if (returnValue.Item1.Equals(false))
                {
                    CreateDatabase(releaseVersion, connectionBuilder); // Database not found - create it
                }
                else if (IsSchemaDifferent(releaseVersion, connectionBuilder))
                {
                    DropDatabase(connectionBuilder.Database);
                    CreateDatabase(releaseVersion, connectionBuilder);
                }
            }


            catch (Exception ex)
            {
                Log.Error("Error in ResetDeveloper", ex);
                return ErrorCode.AuditLogDbError;
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
                return ErrorCode.AuditLogDbError;
            }
            return ErrorCode.NoError;
        }

        private void CreateDatabase(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            var dbSchemaResourceName = GetDbSchemaResourceName(releaseVersion);
            // Create database
            InitializeDatabase(connectionBuilder.Database);

            using (var dbConnection = new NpgsqlConnection(connectionBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(releaseVersion, dbSchemaResourceName));
                InsertSchemaVersion(releaseVersion, dbConnection);
                dbConnection.Close();
            }
            Log.Info($"Created database {connectionBuilder.Database}");
        }

        private bool IsSchemaDifferent(ReleaseVersions releaseVersion, NpgsqlConnectionStringBuilder connectionBuilder)
        {
            var schemaVersionTableName = GetSchemaVersionTableName(releaseVersion);
            var targetSchemaVersion = GetSchemaVersion(releaseVersion);
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

                if ((int)schemaVersion.majorversion != targetSchemaVersion.Major ||
                    (int)schemaVersion.minorversion != targetSchemaVersion.Minor)
                {
                    return true; // Schema version different
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

        private NpgsqlConnectionStringBuilder GenerateConnnectionStringBuilder(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    var auditTrailConnVer15 = ConfigurationManager.AppSettings["AuditTrailConnVer15"];
                    var connectionStringVer15 = ConfigurationManager.ConnectionStrings[auditTrailConnVer15].ConnectionString;
                    return new NpgsqlConnectionStringBuilder(connectionStringVer15);
                case ReleaseVersions.Version16:
                    var auditTrailConnVer16 = ConfigurationManager.AppSettings["AuditTrailConnVer16"];
                    var connectionStringVer16 = ConfigurationManager.ConnectionStrings[auditTrailConnVer16].ConnectionString;
                    return new NpgsqlConnectionStringBuilder(connectionStringVer16);
            }

            throw new NotImplementedException();
        }

        private string GetDbSchemaResourceName(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return AuditTrailResourceNames.AuditTrailDbSchemaVer15;
                case ReleaseVersions.Version16:
                    return AuditTrailResourceNames.AuditTrailDbSchemaVer16;
            }

            throw new NotImplementedException();
        }

        private Version GetSchemaVersion(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                    return SchemaVersions.AuditTrailSchemaVersion15;
                case ReleaseVersions.Version16:
                    return SchemaVersions.AuditTrailSchemaVersion16;
            }
            throw new NotImplementedException();
        }

      

        private string GetSchemaVersionTableName(ReleaseVersions releaseVersion)
        {
            switch (releaseVersion)
            {
                case ReleaseVersions.Version15:
                case ReleaseVersions.Version16:
                    return AuditTrailResourceNames.SchemaVersionTable;
            }
            throw new NotImplementedException();
        }


    }
}
